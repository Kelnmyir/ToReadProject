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
    class BookTester
    {
        private readonly IBookService _service;

        public BookTester(IBookService service)
        {
            _service = service;
        }

        public void TestReading()
        {
            var books = _service.GetAllBooks();
            foreach (var b in books)
                Print(b);
            foreach (var b in books)
            {
                var book = _service.GetBook(b.Id);
                Print(book);
            }
        }

        public void TestCreating()
        {
            var bookToSave = new BookModel { Name = "Just a Book", Annotation = "Book without related entities" };
            _service.Create(bookToSave);

            bookToSave = new BookModel { Name = "Book Located", Annotation = "Shelf and Place", LocationPlace = "Wardrobe", LocationShelf = 1 };
            _service.Create(bookToSave);

            var anyBook = _service.GetAllBooks().FirstOrDefault();
            anyBook = _service.GetBook(anyBook.Id);
            AuthorModel author = anyBook.AuthorModels.FirstOrDefault();
            var authors = new List<AuthorModel> { author };
            bookToSave = new BookModel { Name = "Book with Author", Annotation = "getting Author in a strange way", AuthorModels = authors };
            _service.Create(bookToSave);

            TestReading();
        }

        public void TestDeleting()
        {
            var book1 = _service.GetAllBooks().First();
            _service.Delete(book1);
            var book2 = _service.GetAllBooks().First();
            _service.Delete(book2);
            TestReading();
        }

        public void TestUpdating()
        {
            var books = _service.GetAllBooks().ToList();

            var bookToSave = new BookModel { Id = books[0].Id, Name = "Just a Book", Annotation = "Book without related entities" };
            _service.Update(bookToSave);

            bookToSave = new BookModel { Id = books[1].Id, Name = "Book Located", Annotation = "Shelf and Place", LocationPlace = "Wardrobe", LocationShelf = 1 };
            _service.Update(bookToSave);

            var anyBook = _service.GetAllBooks().ToList()[6];
            anyBook = _service.GetBook(anyBook.Id);
            bookToSave = new BookModel { Id = books[2].Id, Name = "Book with Author", Annotation = "getting Author in a strange way", AuthorModels = anyBook.AuthorModels };
            _service.Update(bookToSave);

            TestReading();
        }

        private void Print(BookModel book)
        {
            Console.WriteLine(book.Id);
            Console.WriteLine($"Name: {book.Name}\t\tAnnotation: {book.Annotation}");
            Console.WriteLine($"Place: {book.LocationPlace}\t\tShelf: {book.LocationShelf}");
            if (book.AuthorModels.Count != 0)
            {
                Console.WriteLine("Authors are:");
                foreach (var a in book.AuthorModels)
                    Console.WriteLine($"{a.FirstName} {a.LastName}");
            }
            else
            {
                Console.WriteLine("No authors for this book");
            }
            if (book.GenreModels.Count != 0)
            {
                Console.WriteLine("Genres are:");
                foreach (var g in book.GenreModels)
                    Console.WriteLine($"{g.Name} {g.Description}");
            }
            else
            {
                Console.WriteLine("No genres for this book");
            }
            Console.WriteLine();
        }
    }
}
