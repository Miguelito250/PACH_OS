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
    public class VentasController : Controller
    {
        private readonly Pach_OSContext _context;

        public VentasController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.Ventas.Include(v => v.Usuario);
            return View(await pach_OSContext.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.IdVentas == id);
            if (venta == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "DetalleVentas", new { id = id });
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {

            ViewBag.ventaId = new SelectList(_context.Ventas, "IdVentas", "IdVentas");
            ViewBag.ProductosId = new SelectList(_context.Productos, "IdProductos", "NomProducto");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVentas,UsuarioId,FechaVenta,TotalVenta,TipoPago,Pago,PagoDomicilio")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "DetalleVentas", new { ventaId = venta.IdVentas });
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.UsuarioId);
            return NotFound();
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.UsuarioId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVentas,UsuarioId,FechaVenta,TotalVenta,TipoPago,Pago,PagoDomicilio")] Venta venta)
        {
            if (id != venta.IdVentas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(venta.Pago >= venta.TotalVenta)
                    {
                        _context.Update(venta);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVentas))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", venta.UsuarioId);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.IdVentas == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'Pach_OSContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return (_context.Ventas?.Any(e => e.IdVentas == id)).GetValueOrDefault();
        }
    }
}
