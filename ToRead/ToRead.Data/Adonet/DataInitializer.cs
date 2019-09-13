using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using System.Linq;

namespace ToRead.Data.Adonet
{
    public class DataInitializer
    {
        private readonly AppContext _context;

        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorsBooksRepository _abRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IGenresBooksRepository _gbRepository;
        private readonly ILocationRepository _locationRepository;

        public DataInitializer(AppContext context)
        {
            _context = context;
            _authorRepository = new AuthorRepository(context);
            _abRepository = new AuthorsBooksRepository(context);
            _bookRepository = new BookRepository(context);
            _genreRepository = new GenreRepository(context);
            _gbRepository = new GenresBooksRepository(context);
            _locationRepository = new LocationRepository(context);
        }

        public void Initialize()
        {
            ClearTable("authorsBooks");
            ClearTable("authors");
            ClearTable("genresBooks");
            ClearTable("genres");
            ClearTable("books");
            ClearTable("locations");
            RefillLocationTable();
            RefillBooksTable();
            RefillAuthorsTable();
            RefillAuthorsBooksTable();
            RefillGenresTable();
            RefillGenresBooksTable();
        }

        private void RefillGenresBooksTable()
        {
            var books = _bookRepository.Get().ToList();
            var genres = _genreRepository.Get().ToList();
            List<GenresBooksEntity> gbs = new List<GenresBooksEntity>
            {
                new GenresBooksEntity { Book = books[0], Genre = genres[2]},
                new GenresBooksEntity { Book = books[1], Genre = genres[2]},
                new GenresBooksEntity { Book = books[1], Genre = genres[3]},
                new GenresBooksEntity { Book = books[3], Genre = genres[2]},
                new GenresBooksEntity { Book = books[3], Genre = genres[3]},
                new GenresBooksEntity { Book = books[5], Genre = genres[1]},
                new GenresBooksEntity { Book = books[6], Genre = genres[1]}
            };
            foreach (var gb in gbs)
            {
                _gbRepository.Create(gb);
            }
        }

        private void RefillGenresTable()
        {
            var genres = new List<GenreEntity>
            {
                new GenreEntity { Name = "SciFi", Description = "Scientific Fiction" },
                new GenreEntity { Name = "Fantasy" },
                new GenreEntity { Name = "Non-fiction" },
                new GenreEntity { Name = "For study"}
            };
            foreach(GenreEntity g in genres)
            {
                _genreRepository.Create(g);
            }
        }

        private void RefillAuthorsBooksTable()
        {
            var authors = _authorRepository.Get().ToList();
            var books = _bookRepository.Get().ToList();
            var abs = new List<AuthorsBooksEntity>
            {
                new AuthorsBooksEntity { Author = authors[0], Book = books[0] },
                new AuthorsBooksEntity { Author = authors[1], Book = books[4] },
                new AuthorsBooksEntity { Author = authors[3], Book = books[3] },
                new AuthorsBooksEntity { Author = authors[4], Book = books[3] },
                new AuthorsBooksEntity { Author = authors[5], Book = books[5] },
                new AuthorsBooksEntity { Author = authors[5], Book = books[6] },
                new AuthorsBooksEntity { Author = authors[6], Book = books[1] },
            };
            foreach (AuthorsBooksEntity ab in abs)
            {
                _abRepository.Create(ab);
            }
        }

        private void RefillAuthorsTable()
        {
            var authors = new List<AuthorEntity>
            {
                new AuthorEntity { FirstName = "Jared", LastName = "Diamond" },
                new AuthorEntity { FirstName = "Artur Conan", LastName = "Doile" },
                new AuthorEntity { FirstName = "George", LastName = "Bush" },
                new AuthorEntity { FirstName = "", LastName = "Билич" },
                new AuthorEntity { FirstName = "", LastName = "Крыжановский" },
                new AuthorEntity { FirstName = "George", LastName = "Martin" },
                new AuthorEntity { FirstName = "Andrew", LastName = "Troelsen" }
            };
            foreach (AuthorEntity a in authors)
            {
                _authorRepository.Create(a);
            }
        }

        private void RefillBooksTable()
        {
            List<LocationEntity> locations = _locationRepository.Get().ToList();
            List<BookEntity> books = new List<BookEntity>
            {
                new BookEntity { Name = "Collapse", Annotation = "How civilizations die or survive" },
                new BookEntity { Name = "Pro C# 7", Annotation = "You know why you need it", Location = locations[1] },
                new BookEntity { Name = "О мышах и людях", Annotation = "Записки экспериментатора", Location = locations[0]},
                new BookEntity { Name = "Биология", Annotation = "Учебник для поступающих", Location = locations[1]},
                new BookEntity { Name = "Sherlock Holmes", Annotation = "You know it", Location = locations[0]},
                new BookEntity { Name = "Ice and Fire Song", Annotation = "Epic thing", Location = locations[1]},
                new BookEntity { Name = "Knight of the Seven Kingdoms", Annotation = "Epic thing", Location = locations[1]}
            };
            foreach (BookEntity b in books)
            {
                _bookRepository.Create(b);
            }
        }

        private void RefillLocationTable()
        {
            List<LocationEntity> locations =new List<LocationEntity>
            {
                new LocationEntity { Place = "Wardrobe", Shelf = 1 },
                new LocationEntity { Place = "Wardrobe", Shelf = 2 },
                new LocationEntity { Place = "Workplace" }
            };
            foreach (LocationEntity location in locations)
            {
                _locationRepository.Create(location);
            }

        }

        private void ClearTable(string tableName)
        {
            string sql = $"DELETE FROM {tableName}";
            _context.StartNonQuery(sql);
            _context.CloseConnection();
        }


    }
}
