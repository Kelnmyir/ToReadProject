using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ToRead.Data;
using ToRead.Data.Models;
using ToRead.Services;
using ToRead.MVC.Models;

namespace ToRead.Services.Testing
{
    class AuthorTester
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public AuthorTester(
            IAuthorService authorService,
            IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public void TestReading()
        {
            var authors = _authorService.GetAllAuthors();
            foreach(AuthorModel author in authors)
                Print(author);
            foreach (var author in authors)
            {
                var authorDetailed = _authorService.GetAuthor(author.Id);
                Print(authorDetailed);
            }
        }

        public void TestCreating()
        {
            var author = new AuthorModel { FirstName = "Null", LastName = "Books" };
            _authorService.Create(author);

            var books = _bookService.GetAllBooks();
            author = new AuthorModel { FirstName = "All B", LastName = "Author", BookModels = books };
            _authorService.Create(author);

            TestReading();
        }

        public void TestDeleting()
        {
            var author1 = _authorService.GetAllAuthors().First();
            _authorService.Delete(author1);

            var author2 = _authorService.GetAllAuthors().First();
            _authorService.Delete(author2);

            TestReading();
        }

        public void TestUpdating()
        {
            var authors = _authorService.GetAllAuthors().ToList();

            var author = new AuthorModel { Id = authors[0].Id, FirstName = "Null", LastName = "Books" };
            _authorService.Update(author);

            var books = _bookService.GetAllBooks();
            author = new AuthorModel { Id = authors[1].Id, FirstName = "All B", LastName = "Author", BookModels = books };
            _authorService.Update(author);

            TestReading();
        }

        private void Print(AuthorModel author)
        {
            Console.WriteLine(author.Id);
            Console.WriteLine($"First Name: {author.FirstName} Last Name: {author.LastName}");
            if (author.BookModels.Count > 0)
            {
                Console.WriteLine("Books are:");
                foreach (var book in author.BookModels)
                {
                    Console.WriteLine($"Name: {book.Name}\t\t Annotation: {book.Annotation}");
                }
            }
            else
            {
                Console.WriteLine("No books for this author");
            }
            Console.WriteLine();
        }
    }
}
