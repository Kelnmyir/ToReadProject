using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ToRead.MVC.Models;

namespace ToRead.MVC.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            var fakeBookList = new List<BookModel>(2) {
                new BookModel() { Name="Pro C# 7", Annotation="You know why you need it"},
                new BookModel() { Name="Репетитор по химии", Annotation="Больше не нужен"},
            };
            return View(fakeBookList);
        }
    }
}