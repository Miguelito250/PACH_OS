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
    public class InsumosController : Controller
    {
        private readonly Pach_OSContext _context;

        public InsumosController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Insumos
        public async Task<IActionResult> Index()
        {
            IEnumerable<Insumo> ListInsumo = _context.Insumos;
            return View(ListInsumo);
        }

        // GET: Insumos/Create
        public IActionResult Create()
        {
            ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor");
            return View();
        }

        // POST: Insumos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Compras");
            }
            ViewData["ProveedoresId"] = new SelectList(_context.Proveedores, "Nit", "Nit", insumo.ProveedoresId);
            return RedirectToAction("Index", "Compras");
        }
    }
}
