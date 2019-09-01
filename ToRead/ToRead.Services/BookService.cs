using System;
using System.Collections.Generic;
using System.Linq;
using ToRead.MVC.Models;
using ToRead.Data;
using ToRead.Data.Models;
using AutoMapper;

namespace ToRead.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorsBooksRepository _abRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IGenresBooksRepository _gbRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public BookService(
            IBookRepository bookRepository, 
            IAuthorRepository authorRepository,
            IAuthorsBooksRepository abRepository,
            IGenreRepository genreRepository,
            IGenresBooksRepository gbRepository,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _repository = bookRepository;
            _authorRepository = authorRepository;
            _abRepository = abRepository;
            _genreRepository = genreRepository;
            _gbRepository = gbRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public void Create(BookModel model)
        {
            var book = _mapper.Map<BookEntity>(model);
            if (model.LocationPlace!=null)
                book.Location = _locationRepository.Get()
                    .Where(l => (l.Place == model.LocationPlace) && (l.Shelf == model.LocationShelf))
                    .Single();
            _repository.Create(book);
            foreach (AuthorModel authorModel in model.AuthorModels)
            {
                var author = _authorRepository.GetOne(authorModel.Id);
                _abRepository.Create(new AuthorsBooksEntity { Author = author, Book = book });
            }
            foreach (GenreModel genreModel in model.GenreModels)
            {
                var genre = _genreRepository.GetOne(genreModel.Id);
                _gbRepository.Create(new GenresBooksEntity { Book = book, Genre = genre });
            }
        }

        public void Delete(BookModel model)
        {
            var book = _repository.GetBookDetailed(model.Id);
            foreach (AuthorsBooksEntity ab in book.AuthorsBooks.ToList())
            {
                _abRepository.Delete(ab);
            }
            foreach (GenresBooksEntity gb in book.GenresBooks.ToList())
            {
                _gbRepository.Delete(gb);
            }
            _repository.Delete(book);
        }

        public ICollection<BookModel> GetAllBooks()
        {
            var books = _repository.Get().ToList();
            return _mapper.Map<ICollection<BookEntity>, ICollection<BookModel>>(books);
        }

        public BookModel GetBook(int id)
        {
            var book = _repository.GetBookDetailed(id);
            BookModel model = _mapper.Map<BookModel>(book);
            foreach (var ab in book.AuthorsBooks)
            {
                model.AuthorModels.Add(_mapper.Map<AuthorModel>(ab.Author));
            }
            foreach (var gb in book.GenresBooks)
            {
                model.GenreModels.Add(_mapper.Map<GenreModel>(gb.Genre));
            }
            return model;
        }

        public void Update(BookModel model)
        {
            var book = _mapper.Map<BookEntity>(model);

            if (model.LocationPlace != null)
                book.Location = _locationRepository.Get()
                    .Where(l => (l.Place == model.LocationPlace) && (l.Shelf == model.LocationShelf))
                    .Single();

            foreach (AuthorsBooksEntity ab in _abRepository.Get().Where(ab => ab.BookId == book.Id))
                _abRepository.Delete(ab);
            foreach (AuthorModel authorModel in model.AuthorModels)
            {
                var author = _authorRepository.GetOne(authorModel.Id);
                _abRepository.Create(new AuthorsBooksEntity { Author = author, Book = book });
            }

            foreach (GenresBooksEntity gb in _gbRepository.Get().Where(gb => gb.BookId == book.Id))
                _gbRepository.Delete(gb);
            foreach (GenreModel genreModel in model.GenreModels)
            {
                var genre = _genreRepository.GetOne(genreModel.Id);
                _gbRepository.Create(new GenresBooksEntity { Book = book, Genre = genre });
            }

            _repository.Update(book);
        }
    }
}
