using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToRead.Data.EF
{
    public class DataInitializer
    {
        private readonly AppContext _context;

        public DataInitializer(AppContext context)
        {
            _context = context;
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

        private void ClearTable(string tableName)
        {
            var rawSqlString = $"DELETE FROM dbo.{tableName}";
            _context.Database.ExecuteSqlCommand(rawSqlString);
        }

        private void RefillBooksTable()
        {
            List<LocationEntity> locations = _context.Set<LocationEntity>().ToList();

            _context.Books.AddRange(new List<BookEntity>
            {
                new BookEntity { Name = "Collapse", Annotation = "How civilizations die or survive" },
                new BookEntity { Name = "Pro C# 7", Annotation = "You know why you need it", Location = locations[1] },
                new BookEntity { Name = "О мышах и людях", Annotation = "Записки экспериментатора", Location = locations[0]},
                new BookEntity { Name = "Биология", Annotation = "Учебник для поступающих", Location = locations[1]},
                new BookEntity { Name = "Sherlock Holmes", Annotation = "You know it", Location = locations[0]},
                new BookEntity { Name = "Ice and Fire Song", Annotation = "Epic thing", Location = locations[1]},
                new BookEntity { Name = "Knight of the Seven Kingdoms", Annotation = "Epic thing", Location = locations[1]}
            });
            _context.SaveChanges();
        }

        private void RefillLocationTable()
        {
            _context.Locations.AddRange(new List<LocationEntity>
            {
                new LocationEntity { Place = "Wardrobe", Shelf = 1 },
                new LocationEntity { Place = "Wardrobe", Shelf = 2 },
                new LocationEntity { Place = "Workplace" }
            });
            _context.SaveChanges();
        }

        private void RefillAuthorsTable()
        {
            _context.Authors.AddRange(new List<AuthorEntity>
            {
                new AuthorEntity { FirstName = "Jared", LastName = "Diamond" },
                new AuthorEntity { FirstName = "Artur Conan", LastName = "Doile" },
                new AuthorEntity { FirstName = "George", LastName = "Bush" },
                new AuthorEntity { FirstName = "", LastName = "Билич" },
                new AuthorEntity { FirstName = "", LastName = "Крыжановский" },
                new AuthorEntity { FirstName = "George", LastName = "Martin" },
                new AuthorEntity { FirstName = "Andrew", LastName = "Troelsen" }
            });
            _context.SaveChanges();
        }

        private void RefillAuthorsBooksTable()
        {
            var authors = _context.Authors.ToList();
            var books = _context.Books.ToList();
            _context.AuthorsBooks.AddRange(new List<AuthorsBooksEntity>
            {
                new AuthorsBooksEntity { Author = authors[0], Book = books[0] },
                new AuthorsBooksEntity { Author = authors[1], Book = books[4] },
                new AuthorsBooksEntity { Author = authors[3], Book = books[3] },
                new AuthorsBooksEntity { Author = authors[4], Book = books[3] },
                new AuthorsBooksEntity { Author = authors[5], Book = books[5] },
                new AuthorsBooksEntity { Author = authors[5], Book = books[6] },
                new AuthorsBooksEntity { Author = authors[6], Book = books[1] },
            });
            _context.SaveChanges();
        }

        private void RefillGenresTable()
        {
            _context.Genres.AddRange(new List<GenreEntity>
            {
                new GenreEntity { Name = "SciFi", Description = "Scientific Fiction" },
                new GenreEntity { Name = "Fantasy" },
                new GenreEntity { Name = "Non-fiction" },
                new GenreEntity { Name = "For study"}
            });
            _context.SaveChanges();
        }

        private void RefillGenresBooksTable()
        {
            var books = _context.Books.ToList();
            var genres = _context.Genres.ToList();
            _context.GenresBooks.AddRange(new List<GenresBooksEntity>
            {
                new GenresBooksEntity { Book = books[0], Genre = genres[2]},
                new GenresBooksEntity { Book = books[1], Genre = genres[2]},
                new GenresBooksEntity { Book = books[1], Genre = genres[3]},
                new GenresBooksEntity { Book = books[3], Genre = genres[2]},
                new GenresBooksEntity { Book = books[3], Genre = genres[3]},
                new GenresBooksEntity { Book = books[5], Genre = genres[1]},
                new GenresBooksEntity { Book = books[6], Genre = genres[1]}
            });
            _context.SaveChanges();
        }
    }
}
