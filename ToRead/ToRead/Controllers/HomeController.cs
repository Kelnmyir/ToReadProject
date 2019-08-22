using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToRead.Data;

namespace ToRead.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.AppContext _context;

        public HomeController(Data.AppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var init = new DataInitializer(_context);
            init.Initialize();
            return View();
        }
    }
}