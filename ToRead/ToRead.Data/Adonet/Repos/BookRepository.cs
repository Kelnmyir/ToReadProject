using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using System.Linq;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class BookRepository : Repository<BookEntity>, IBookRepository
    {
        public BookRepository(AppContext context) : base(context) { }

        public BookEntity GetBookDetailed(int id)
        {
            var book = this.GetOne(id);

            string locationQuery =$@"SELECT 
                    locations.Id AS Id, 
                    locations.Place AS Place, 
                    locations.Shelf AS Shelf 
                FROM books 
                INNER JOIN locations 
                    ON books.locationId = locations.Id
                WHERE books.Id = {book.Id}";
            var locationReader = _context.StartReader(locationQuery);
            if (locationReader.HasRows)
            {
                locationReader.Read();
                var location = new LocationEntity();
                foreach (var property in typeof(LocationEntity).GetProperties())
                {
                    if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string))
                        && (!Convert.IsDBNull(locationReader[property.Name])))
                    {
                        property.SetValue(location, locationReader[property.Name]);
                    }
                }
                book.Location = location;
            }
            else
                book.Location = null;
            _context.CloseConnection();

            string authorsQuery = $@"SELECT 
                    authors.Id AS Id, 
                    authors.FirstName AS FirstName, 
                    authors.LastName AS LastName 
                FROM books 
                INNER JOIN authorsBooks 
                    ON books.Id = authorsBooks.BookId
				INNER JOIN authors
					ON authorsBooks.AuthorId = authors.Id
				WHERE books.Id = {book.Id}";
            var authorReader = _context.StartReader(authorsQuery);
            while (authorReader.Read())
            {
                var author = new AuthorEntity();
                foreach (var property in typeof(AuthorEntity).GetProperties())
                {
                    if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string))
                        && (!Convert.IsDBNull(authorReader[property.Name])))
                    {
                        property.SetValue(author, authorReader[property.Name]);
                    }
                }
                book.AuthorsBooks.Add(new AuthorsBooksEntity {BookId = book.Id, AuthorId = author.Id, Book = book, Author = author });
            }
            _context.CloseConnection();

            string genresQuery = $@"SELECT 
                    genres.Id AS Id, 
                    genres.Name AS Name, 
                    genres.Description AS Description 
                FROM books 
                INNER JOIN genresBooks 
                    ON books.Id = genresBooks.BookId
				INNER JOIN genres
					ON genresBooks.GenreId = genres.Id
				WHERE books.Id = {book.Id}";
            var genresReader = _context.StartReader(genresQuery);
            while (genresReader.Read())
            {
                var genre = new GenreEntity();
                foreach (var property in typeof(GenreEntity).GetProperties())
                {
                    if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string))
                        && (!Convert.IsDBNull(genresReader[property.Name])))
                    {
                        property.SetValue(genre, genresReader[property.Name]);
                    }
                }
                book.GenresBooks.Add(new GenresBooksEntity { BookId = book.Id, GenreId = genre.Id, Book = book, Genre = genre });
            }
            _context.CloseConnection();

            return book;
        }
    }
}
