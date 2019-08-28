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
    public class AuthorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var authorModels = _mapper.Map<List<AuthorEntity>, List<AuthorModel>>(_repository.Get().ToList());
            return View(authorModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AuthorModel model)
        {
            var author = _mapper.Map<AuthorEntity>(model);
            _repository.Create(author);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var author = _repository.GetOne(id);
            var model = _mapper.Map<AuthorModel>(author);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(AuthorModel model)
        {
            var author = _mapper.Map<AuthorEntity>(model);
            _repository.Update(author);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var author = _repository.GetAuthorDetailed(id);
            var model = _mapper.Map<AuthorModel>(author);
            var books = new List<BookEntity>();
            foreach (AuthorsBooksEntity ab in author.AuthorsBooks)
            {
                books.Add(ab.Book);
            }
            model.BookModels = _mapper.Map<ICollection<BookEntity>, ICollection<BookModel>>(books);
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = _repository.GetOne(id);
            _repository.Delete(author);
            return RedirectToAction("Index");
        }
    }
}
