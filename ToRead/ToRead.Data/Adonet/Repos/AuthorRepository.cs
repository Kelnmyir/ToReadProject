using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using System.Linq;
using System.Data.SqlClient;


namespace ToRead.Data.Adonet
{
    public class AuthorRepository : Repository<AuthorEntity>, IAuthorRepository
    {
        public AuthorEntity GetAuthorDetailed(int id)
        {
            var author = this.GetOne(id);

            _connection.Open();

            string bookQuery = $@"SELECT
                    books.Id AS Id,
                    books.Name AS Name,
                    books.Annotation AS Annotation
                FROM authors
                INNER JOIN authorsBooks
                    ON authorsBooks.AuthorId = authors.Id
                INNER JOIN books
                    ON books.Id = authorsBooks.BookId
                WHERE authors.Id = {author.Id}";
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
                author.AuthorsBooks.Add(new AuthorsBooksEntity { AuthorId = author.Id, Author = author, BookId = book.Id, Book = book });
            }

            _connection.Close();
            return author;
        }
    }
}
