using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using ToRead.MVC.Models;
using ToRead.Data;
using ToRead.Data.Models;

namespace ToRead.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Book> _repository;

        public BookController(IRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _repository.Get().ToList();
            var bookModels = new List<BookModel>();
            foreach(Book book in books)
            {
                bookModels.Add(_mapper.Map<BookModel>(book));
            }
            return View(bookModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookModel model)
        {
            var book = _mapper.Map<Data.Models.Book>(model);
            _repository.Create(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update (int id)
        {
            var book = _repository.GetOne(id);
            var model = _mapper.Map<MVC.Models.BookModel>(book);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update (BookModel model)
        {
            var book = _mapper.Map<Data.Models.Book>(model);
            _repository.Update(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _repository.GetOne(id);
            _repository.Delete(book);
            return RedirectToAction("Index");
        }
    }
}