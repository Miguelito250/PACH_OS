﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pach_OS.Models;

namespace Pach_OS.Controllers
{
    public class ProductosInsumoesController : Controller
    {
        private readonly Pach_OSContext _context;

        public ProductosInsumoesController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: ProductosInsumoes
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Productoes");
        }
        public async Task<IActionResult> ProductosCreate()
        {
            return RedirectToAction("Create");
        }

        // GET: ProductosInsumoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductosInsumos == null)
            {
                return NotFound();
            }

            var productosInsumo = await _context.ProductosInsumos
                .Include(p => p.Insumos)
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productosInsumo == null)
            {
                return NotFound();
            }

            return View(productosInsumo);
        }

     
        // GET: ProductosInsumoes/Create
        public IActionResult Create(int id )
        {
            ViewBag.productosInsumo = id;
            var productosInsumoList = _context.ProductosInsumos.Where(d => d.ProductosId == id).ToList();

            ViewBag.detallesProInsumos = productosInsumoList;
            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos");
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos");
            return View("Create");
        }

        // POST: ProductosInsumoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductosId,InsumosId,CantInsumo")] ProductosInsumo productosInsumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productosInsumo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "ProductosInsumoes", new { idProInsumo = productosInsumo.ProductosId });
            }

            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos", productosInsumo.InsumosId);
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", productosInsumo.ProductosId);
            return View(productosInsumo);
        }


        // GET: ProductosInsumoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductosInsumos == null)
            {
                return NotFound();
            }

            var productosInsumo = await _context.ProductosInsumos.FindAsync(id);
            if (productosInsumo == null)
            {
                return NotFound();
            }
            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos", productosInsumo.InsumosId);
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", productosInsumo.ProductosId);
            return View(productosInsumo);
        }

        // POST: ProductosInsumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductosId,InsumosId,CantInsumo,Id")] ProductosInsumo productosInsumo)
        {
            if (id != productosInsumo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosInsumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosInsumoExists(productosInsumo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsumosId"] = new SelectList(_context.Insumos, "IdInsumos", "IdInsumos", productosInsumo.InsumosId);
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", productosInsumo.ProductosId);
            return View(productosInsumo);
        }

        // GET: ProductosInsumoes/Delete/5
        public async Task<IActionResult> Delete(int? id, int? ProductosId)
        {
            if (id == null || _context.ProductosInsumos == null)
            {
                return NotFound();
            }

            var productosInsumo = await _context.ProductosInsumos
                .Include(p => p.Insumos)
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productosInsumo == null)
            {
                return NotFound();
            }

            return View(productosInsumo);
        }

        // POST: ProductosInsumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int ProductosId)
        {

            if (_context.ProductosInsumos == null)
            {
                return Problem("Entity set 'Pach_OSContext.ProductosInsumos'  is null.");
            }
            var productosInsumo = await _context.ProductosInsumos.FindAsync(id);
            if (productosInsumo != null)
            {
                _context.ProductosInsumos.Remove(productosInsumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", new { id = productosInsumo.ProductosId = ProductosId });
        }

        private bool ProductosInsumoExists(int id)
        {
          return (_context.ProductosInsumos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
