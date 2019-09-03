using System;
using System.Collections.Generic;
using System.Linq;
using ToRead.MVC.Models;
using ToRead.Data;
using ToRead.Data.Models;
using AutoMapper;

namespace ToRead.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorsBooksRepository _abRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public AuthorService(
            IAuthorRepository authorRepository,
            IAuthorsBooksRepository abRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _abRepository = abRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public void Create(AuthorModel model)
        {
            var author = _mapper.Map<AuthorEntity>(model);
            _authorRepository.Create(author);
            foreach(var bookModel in model.BookModels)
            {
                var book = _bookRepository.GetOne(bookModel.Id);
                _abRepository.Create(new AuthorsBooksEntity { Author = author, Book = book });
            }
        }

        public void Delete(AuthorModel model)
        {
            var author = _authorRepository.GetAuthorDetailed(model.Id);
            foreach (AuthorsBooksEntity ab in author.AuthorsBooks.ToList())
            {
                _abRepository.Delete(ab);
            }
            _authorRepository.Delete(author);
        }

        public ICollection<AuthorModel> GetAllAuthors()
        {
            var authors = _authorRepository.Get().ToList();
            return _mapper.Map<List<AuthorEntity>, ICollection<AuthorModel>>(authors);
        }

        public AuthorModel GetAuthor(int id)
        {
            var author = _authorRepository.GetAuthorDetailed(id);
            var model = _mapper.Map<AuthorModel>(author);
            foreach (var ab in author.AuthorsBooks)
            {
                var bookModel = _mapper.Map<BookModel>(ab.Book);
                model.BookModels.Add(bookModel);
            }
            return model;
        }

        public void Update(AuthorModel model)
        {
            var author = _mapper.Map<AuthorEntity>(model);

            foreach (AuthorsBooksEntity ab in _abRepository.Get().Where(ab => ab.AuthorId == author.Id))
                _abRepository.Delete(ab);
            foreach (var bookModel in model.BookModels)
            {
                var book = _bookRepository.GetOne(bookModel.Id);
                _abRepository.Create(new AuthorsBooksEntity { Author = author, Book = book });
            }
            _authorRepository.Update(author);
        }
    }
}
