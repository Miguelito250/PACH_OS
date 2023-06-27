using Microsoft.AspNetCore.Mvc;
using Pach_OS.Models;
using Pach_OS.Models.ViewModels;
using System.Diagnostics;

namespace Pach_OS.Controllers
{
    public class HomeController : Controller
    {
        private readonly Pach_OSContext _context;

        public HomeController(Pach_OSContext context)
        {
            _context = context;
        }
        public IActionResult ResumenVenta()
{
            DateTime fechainicion = DateTime.Now;
            fechainicion = fechainicion.AddDays(-5);
            List<VMVenta> Lista = (from tbventa in _context.Ventas
                                    where tbventa.FechaVenta.Value.Date >= fechainicion.Date
                                    group tbventa by tbventa.FechaVenta.Value.Date into grupo
                                    select new VMVenta
                                    {
                                        fecha = grupo.Key.ToString("dd/MM/yyyy"),
                                        pago = grupo.Count(),
                                    }).ToList();

            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}