using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pach_OS.Models;

namespace Pach_OS.Controllers
{
    public class DetallesComprasController : Controller
    {
        private readonly Pach_OSContext _context;

        public DetallesComprasController(Pach_OSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.DetallesCompras.Include(d => d.Compras).Include(d => d.Insumos);
            return View(await pach_OSContext.ToListAsync());
        }

        // GET: Login
        //public async Task<IActionResult> Index()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Redirect("/Identity/Account/Login");
        //    }
        //    else
        //    {
        //        var pach_OSContext = _context.DetallesCompras.Include(d => d.Compras).Include(d => d.Insumos);
        //        return View(await pach_OSContext.ToListAsync());
        //    }
        //}

        // GET: DetallesCompras/Create
        public IActionResult Create(int compraId)
        {
            ViewBag.IdCompraDll = compraId;
            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos");
            var DetalleCompra = _context.DetallesCompras.Where(d => d.ComprasId == compraId).ToList();
            ViewBag.DetalleCompra = DetalleCompra;
            ViewData["ComprasId"] = new SelectList(_context.Compras, "IdCompras", "IdCompras");
            return View(new DetallesCompra { ComprasId = compraId });
        }

        // POST: DetallesCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallesCompras,ComprasId,InsumosId,PrecioInsumo,Cantidad")] DetallesCompra detallesCompra)
        {
            if (ModelState.IsValid)
            {
                var insumos = _context.Insumos.FirstOrDefault(c => c.IdInsumos == detallesCompra.InsumosId);
                detallesCompra.PrecioInsumo = detallesCompra.PrecioInsumo * detallesCompra.Cantidad;
                if (insumos != null)
                {
                    _context.Add(detallesCompra);
                    await _context.SaveChangesAsync();

                    insumos.CantInsumo += detallesCompra.Cantidad;
                    _context.Insumos.Update(insumos);

                    var compras = await _context.Compras.FindAsync(detallesCompra.ComprasId);
                    compras.Total = 0;
                    foreach (var item in await _context.DetallesCompras.Where(c => c.ComprasId == detallesCompra.ComprasId).ToListAsync())
                    {
                        compras.Total += item.PrecioInsumo;
                    }
                    _context.Compras.Update(compras);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", "DetallesCompras", new { compraId = detallesCompra.ComprasId });

                }
            }
            ViewData["ComprasId"] = new SelectList(_context.Compras, "IdCompras", "IdCompras", detallesCompra.ComprasId);
            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos", detallesCompra.InsumosId);
            return RedirectToAction("Create", "DetallesCompras", new { compraId = detallesCompra.ComprasId });
        }
    }
}
