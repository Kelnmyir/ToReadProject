using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using ToRead_DAL;
using ToRead_DAL.Models;

namespace ToRead_DAL.DataInitialization
{
    public class MyDataInitializer
    {
        public static void InitializeData(ToReadContext context)
        {
            var authors = new List<Author>
            {
                new Author{FirstName="Ольга",LastName="Громыко"},
                new Author{FirstName="Александр",LastName="Дюма"},
                new Author{FirstName="Андрей",LastName="Уланов"}
            };
            context.Authors.AddRange(authors);
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book{Name="Космобиолухи"},
                new Book{Name="Космопсихолухи"},
                new Book{Name="Плюс на минус"},
                new Book{Name="Три мушкетера"},
                new Book{Name="Коллапс"},
                new Book{Name="Le petit Larousse"}
            };
            context.Books.AddRange(books);
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre{Name="Fantasy"},
                new Genre {Name="Sci-Fi"},
                new Genre {Name="Classic fiction"},
                new Genre {Name="Encyclopaedia"},
                new Genre {Name="Sci-Pop"},
                new Genre {Name="Magazine"}
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location{ Name = "Bookshelf 1", Books = new List<Book>{ books[0],books[1],books[2] } },
                new Location{ Name = "Bookshelf 2", Books = new List<Book>{ books[3],books[5] } }
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();

            var authorBooks = new List<AuthorBook>
            {
                new AuthorBook{Author=authors[0],Book=books[0]},
                new AuthorBook{Author=authors[0],Book=books[1]},
                new AuthorBook{Author=authors[0],Book=books[2]},
                new AuthorBook{Author=authors[1],Book=books[3]},
                new AuthorBook{Author=authors[2],Book=books[2]},
            };
            context.AuthorBooks.AddRange(authorBooks);
            context.SaveChanges();

            var bookGenres = new List<BookGenre>
            {
                new BookGenre{Book=books[0],Genre=genres[1]},
                new BookGenre{Book=books[1],Genre=genres[1]},
                new BookGenre{Book=books[2],Genre=genres[0]},
                new BookGenre{Book=books[3],Genre=genres[2]},
                new BookGenre{Book=books[4],Genre=genres[4]},
                new BookGenre{Book=books[5],Genre=genres[3]},
            };
            context.BookGenres.AddRange(bookGenres);
            context.SaveChanges();
        }

        public static void RecreateDatabase(ToReadContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static void ClearData(ToReadContext context)
        {
            ExecuteDeleteSql(context, "Authors");
            ExecuteDeleteSql(context, "AuthorBooks");
            ExecuteDeleteSql(context, "Books");
            ExecuteDeleteSql(context, "BookGenres");
            ExecuteDeleteSql(context, "Genres");
            ExecuteDeleteSql(context, "Locations");
            ResetIdentity(context);
        }

        private static void ExecuteDeleteSql(ToReadContext context, string tableName)
        {
            var rawSql = $"Delete * from dbo.{tableName}";
            context.Database.ExecuteSqlCommand(rawSql);
        }

        private static void ResetIdentity(ToReadContext context)
        {
            var tables = new[] { "Authors", "AuthorBooks", "Books", "BookGenres", "Genres", "Locations" };
            foreach (var table in tables)
            {
                var rawSql = $"DBCC CHECKIDENT (\"dbo.{table}\", RESEED, -1";
                context.Database.ExecuteSqlCommand(rawSql);
            }
        }
    }
}