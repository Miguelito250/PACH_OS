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
    public class ComprasController : Controller
    {
        private readonly Pach_OSContext _context;

        public ComprasController(Pach_OSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.Compras.Include(c => c.IdProveedorNavigation).Include(c => c.IdUsuarioNavigation);
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
        //        var pach_OSContext = _context.Compras.Include(c => c.IdProveedorNavigation).Include(c => c.IdUsuarioNavigation);
        //        return View(await pach_OSContext.ToListAsync());
        //    }
        //}

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdProveedorNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCompras == id);
            if (compra == null)
            {
                return NotFound();
            }
            compra.DetallesCompras = await _context.DetallesCompras
                .Where(x => x.ComprasId == compra.IdCompras)
                .Include(v => v.Insumos)
                .ToListAsync();
            return RedirectToAction("Create", "DetallesCompras", new { compraId = compra.IdCompras });
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompras,IdUsuario,FechaCompra,Total,IdProveedor")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "DetallesCompras", new { compraId = compra.IdCompras });
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedor);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", compra.IdUsuario);
            return View(compra);
        }
    }
}
