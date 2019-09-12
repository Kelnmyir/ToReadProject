using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class GenreRepository : Repository<GenreEntity>, IGenreRepository
    {
        public GenreRepository(AppContext context) : base(context) { }

        public GenreEntity GetGenreDetailed(int id)
        {
            var genre = this.GetOne(id);

            string bookQuery = $@"SELECT
                    books.Id AS Id,
                    books.Name AS Name,
                    books.Annotation AS Annotation
                FROM genres
                INNER JOIN genresBooks
                    ON genresBooks.GenreId = genres.Id
                INNER JOIN books
                    ON books.Id = genresBooks.BookId
                WHERE genres.Id = {genre.Id}";
            var bookReader = _context.StartReader(bookQuery);

            while (bookReader.Read())
            {
                var book = new BookEntity();
                foreach (var property in typeof(BookEntity).GetProperties())
                {
                    if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string))
                        && (!Convert.IsDBNull(bookReader[property.Name])))
                    {
                        property.SetValue(book, bookReader[property.Name]);
                    }
                }
                genre.GenresBooks.Add(new GenresBooksEntity { GenreId = genre.Id, Genre = genre, BookId = book.Id, Book = book });
            }

            _context.CloseConnection();
            return genre;
        }
    }
}
