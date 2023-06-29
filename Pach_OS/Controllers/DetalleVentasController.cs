using System;
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
            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "NomProducto");
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
        public async Task<IActionResult> Create(DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                var producto = _context.Productos.FirstOrDefault(p => p.IdProductos == detalleVenta.ProductosId);

                if (producto != null)
                {
                    var receta = _context.ProductosInsumos.Where(pi => pi.ProductosId == producto.IdProductos).ToList();

                    foreach (var insumoReceta in receta)
                    {
                        var insumo = _context.Insumos.FirstOrDefault(i => i.IdInsumos == insumoReceta.InsumosId);
                        var cantidadInsumo = insumo != null ? insumo.CantInsumo : 0;
                        var cantidadNecesaria = insumoReceta.CantInsumo * detalleVenta.CantVendida;

                        if (cantidadNecesaria > cantidadInsumo)
                        {
                            TempData["SuccessMessage"] = $"No hay suficientes insumos ({insumo?.NomInsumo}) disponibles para este producto.";
                            return RedirectToAction("Create", "DetalleVentas", new { ventaId = detalleVenta.VentaId });
                        }

                        // Actualizar la cantidad de insumo en la tabla Insumos
                        insumo.CantInsumo -= cantidadNecesaria;
                        _context.Update(insumo);
                    }

                    _context.Add(detalleVenta);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Create", "DetalleVentas", new { ventaId = detalleVenta.VentaId });
                }
                else
                {
                    ModelState.AddModelError("", "El producto seleccionado no existe.");
                }
            }

            ViewData["ProductosId"] = new SelectList(_context.Productos, "IdProductos", "NomProducto", detalleVenta.ProductosId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "IdVentas", "IdVentas", detalleVenta.VentaId);

            return RedirectToAction("Create", "DetalleVentas", new { ventaId = detalleVenta.VentaId });
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

        // GET: DetalleVentas/ConfirmarVenta/5
        public async Task<IActionResult> ConfirmarVenta(int? id, DetalleVenta detalleVenta)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            // Cargar los detalles de venta incluyendo la propiedad de navegación "Productos"
            var detallesVenta = await _context.DetalleVentas
                .Include(d => d.Productos)
                .Where(d => d.VentaId == id)
                .ToListAsync();

            // Calcula el total de la venta sumando los precios totales de los detalles de venta
            int totalVenta = (int)detallesVenta.Sum(d => d.Productos.PrecioVenta * d.CantVendida);

            // Obtén el monto del pago a domicilio y verifica si es nulo
            int pagoDomicilio = venta.PagoDomicilio ?? 0;

            // Agrega el monto del pago a domicilio al total de la venta
            int totalConDomicilio = totalVenta + pagoDomicilio;

            // Asigna el total de la venta a la propiedad TotalVenta del modelo Venta
            venta.TotalVenta = totalConDomicilio;

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", "Ventas", new { id = id });
        }




        // POST: DetalleVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleVenta,VentaId,CantVendida,ProductosId, Precio, TotalPrecio")] DetalleVenta detalleVenta)
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
            return RedirectToAction("Create", "DetalleVentas", new { ventaId = detalleVenta.VentaId });
        }

        private bool DetalleVentaExists(int id)
        {
            return (_context.DetalleVentas?.Any(e => e.IdDetalleVenta == id)).GetValueOrDefault();
        }
    }
}
