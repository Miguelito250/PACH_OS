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
    public class DetalleVentasController : Controller
    {
        private readonly Pach_OSContext _context;

        public DetalleVentasController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.DetalleVentas.Include(d => d.Productos).Include(d => d.Venta);
            return View(await pach_OSContext.ToListAsync());
        }

        // GET: DetalleVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetalleVentas == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.Productos)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        //GET: DetalleVentas/Create
        public IActionResult Create(int ventaId)
        {
            ViewBag.idVentaDet = ventaId;
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos");
            var detalleVenta = _context.DetalleVentas.Where(d => d.VentaId == ventaId).ToList();
            ViewBag.detalleVenta = detalleVenta;
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVentas", "IdVentas");
            return View(new DetalleVenta { VentaId = ventaId }); // Pasar el objeto DetalleVenta con el valor de ventaId asignado
        }

        // POST: DetalleVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleVenta,VentaId,CantVendida,ProductosId")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "DetalleVentas", new { ventaId = detalleVenta.VentaId});
            }
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", detalleVenta.ProductosId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVentas", "IdVentas", detalleVenta.VentaId);
            return NotFound();
        }

        // GET: DetalleVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetalleVentas == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", detalleVenta.ProductosId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVentas", "IdVentas", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleVenta,VentaId,CantVendida,ProductosId")] DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.IdDetalleVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.IdDetalleVenta))
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
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "IdProductos", detalleVenta.ProductosId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVentas", "IdVentas", detalleVenta.VentaId);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetalleVentas == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.Productos)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetalleVentas == null)
            {
                return Problem("Entity set 'Pach_OSContext.DetalleVentas'  is null.");
            }
            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta != null)
            {
                _context.DetalleVentas.Remove(detalleVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentaExists(int id)
        {
          return (_context.DetalleVentas?.Any(e => e.IdDetalleVenta == id)).GetValueOrDefault();
        }
    }
}
