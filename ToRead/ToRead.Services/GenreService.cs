using System;
using System.Collections.Generic;
using System.Linq;
using ToRead.MVC.Models;
using ToRead.Data;
using ToRead.Data.Models;
using AutoMapper;

namespace ToRead.Services
{
    public class GenreService : IGenreService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenresBooksRepository _gbRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(
            IBookRepository bookRepository,
            IGenresBooksRepository gbRepository,
            IGenreRepository genreRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _gbRepository = gbRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public void Create(GenreModel model)
        {
            var genre = _mapper.Map<GenreEntity>(model);
            _genreRepository.Create(genre);
            foreach(var bookModel in model.BookModels)
            {
                var book = _bookRepository.GetOne(bookModel.Id);
                _gbRepository.Create(new GenresBooksEntity { Book = book, Genre = genre });
            }
        }

        public void Delete(GenreModel model)
        {
            var genre = _genreRepository.GetGenreDetailed(model.Id);
            foreach (var gb in genre.GenresBooks.ToList())
            {
                _gbRepository.Delete(gb);
            }
            _genreRepository.Delete(genre);
        }

        public ICollection<GenreModel> GetAllGenres()
        {
            var genres = _genreRepository.Get().ToList();
            return _mapper.Map<ICollection<GenreEntity>, ICollection<GenreModel>>(genres);
        }

        public GenreModel GetGenre(int id)
        {
            var genre = _genreRepository.GetGenreDetailed(id);
            var genreModel = _mapper.Map<GenreModel>(genre);
            foreach(var gb in genre.GenresBooks)
            {
                genreModel.BookModels.Add(_mapper.Map<BookModel>(gb.Book));
            }
            return genreModel;
        }

        public void Update(GenreModel model)
        {
            var genre = _mapper.Map<GenreEntity>(model);

            foreach (var gb in _gbRepository.Get().Where(gb => gb.GenreId == genre.Id))
                _gbRepository.Delete(gb);
            foreach (var bookModel in model.BookModels)
            {
                var book = _bookRepository.GetOne(bookModel.Id);
                _gbRepository.Create(new GenresBooksEntity { Book = book, Genre = genre });
            }
            _genreRepository.Update(genre);
        }
    }
}
