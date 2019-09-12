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
        public GenreEntity GetGenreDetailed(int id)
        {
            var genre = this.GetOne(id);

            _connection.Open();

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
            SqlCommand cmd = new SqlCommand(bookQuery, _connection);
            var bookReader = cmd.ExecuteReader();
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

            _connection.Close();
            return genre;
        }
    }
}
