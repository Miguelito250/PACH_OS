using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pach_OS.Models;
using System.Diagnostics;
using Pach_OS.Models;

namespace Pach_OS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Index()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Redirect("/Identity/Account/Login");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

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