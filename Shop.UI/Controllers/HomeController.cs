using Microsoft.AspNetCore.Mvc;
using Shop.UI.Models;
using System.Diagnostics;

namespace Shop.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}