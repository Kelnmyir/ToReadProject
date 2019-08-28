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
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bookModels = _mapper.Map<List<BookEntity>, List<BookModel>>(_repository.Get().ToList());
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
            var book = _mapper.Map<Data.Models.BookEntity>(model);
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
            var book = _mapper.Map<Data.Models.BookEntity>(model);
            _repository.Update(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details (int id)
        {
            var book = _repository.GetBookDetailed(id);
            var model = _mapper.Map<MVC.Models.BookModel>(book);
            var authors = new List<AuthorEntity>();
            foreach(AuthorsBooksEntity ab in book.AuthorsBooks)
            {
                authors.Add(ab.Author);
            }
            model.AuthorModels = _mapper.Map<ICollection<AuthorEntity>, ICollection<AuthorModel>>(authors);
            return View(model);
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