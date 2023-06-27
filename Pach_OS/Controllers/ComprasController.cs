using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pach_OS.Models;

namespace Pach_OS.Controllers
{
    public class ComprasController : Controller
    {
        private readonly Pach_OSContext _context;

        public ComprasController(Pach_OSContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Compra> ListCompras = _context.Compras.Include(c => c.IdProveedorNavigation).Include(c => c.IdUsuarioNavigation);
            return View(ListCompras);
        }

        public async Task<IActionResult> Details(int? IdCompra)
        {
            if (IdCompra == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompras == IdCompra);
            if (compra == null)
            {
                return NotFound();
            }
            compra.DetallesCompras = await _context.DetallesCompras
                .Where(x => x.ComprasId == compra.IdCompras)
                .Include(v => v.Insumos)
                .ToListAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", "NumDocumento");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", "Nit");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateDetalles", "Compras", new { compraId = compra.IdCompras });
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "Nit", "Nit", compra.IdProveedor);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "NumDocumento", "NumDocumento", compra.IdUsuario);
            return View(compra);
        }

        public IActionResult CreateDetalles()
        {
            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetalles(DetallesCompra detalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle);
                await _context.SaveChangesAsync();

                var insumo = _context.Insumos.FirstOrDefault(c => c.IdInsumos == detalle.InsumosId);

                if (insumo != null)
                {
                    detalle.PrecioInsumo = detalle.PrecioInsumo * detalle.Cantidad;

                    insumo.CantInsumo += detalle.Cantidad;
                    _context.Insumos.Update(insumo);

                    var compras = await _context.Compras.FindAsync(detalle.ComprasId);
                    compras.Total = 0;
                    foreach (var item in await _context.DetallesCompras.Where(c => c.ComprasId == detalle.ComprasId).ToListAsync())
                    {
                        compras.Total += item.PrecioInsumo;
                    }
                    _context.Compras.Update(compras);

                    await _context.SaveChangesAsync();

                }

                return RedirectToAction("Index");
            }

            ViewData["InsumosId"] = new SelectList(_context.Insumos, "NomInsumo", "NomInsumo", detalle.InsumosId);
            return RedirectToAction("Index");
        }
    }
}
