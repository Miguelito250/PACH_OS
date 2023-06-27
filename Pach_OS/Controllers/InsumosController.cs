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
        public async Task<IActionResult> Index()
        {
            return _context.Insumos != null ?
                        View(await _context.Insumos.ToListAsync()) :
                        Problem("Entity set 'Pach_OSContext.Insumos'  is null.");
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
        //        return _context.Insumos != null ?
        //                View(await _context.Insumos.ToListAsync()) :
        //                Problem("Entity set 'Pach_OSContext.Insumos'  is null.");
        //    }
        //}

        // GET: Insumos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insumos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumos,NomInsumo,CantInsumo,ProveedoresId,TiempoLlegado")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Compras");
            }
            return RedirectToAction("Index", "Compras");
        }
    }
}
