using System;
using System.Threading;
using ToRead.Data;
using ToRead.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace ToRead.Data.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = CreateContext();

            DataInitializer initializer = new DataInitializer(context);
            initializer.Initialize();

            TestReading(context);

            Console.ReadKey();

            TestUpdating(context);

            Console.ReadKey();

            TestCreating(context);

            Console.ReadKey();
        }

        private static void TestReading(AppContext context)
        {
            Console.WriteLine("Test Reading");
            var bookRepo = new BookRepository(context);
            Console.WriteLine("\nAll books:");
            foreach (BookEntity book in bookRepo.Get())
            {
                BookEntity bookDetailed = bookRepo.GetBookDetailed(book.Id);
                Console.WriteLine($"Name: {bookDetailed.Name}\nAnnotation: {bookDetailed.Annotation}");
                if (bookDetailed.Location != null)
                    Console.WriteLine($"Place: {bookDetailed.Location.Place}\nShelf: {bookDetailed.Location.Shelf}");
                else Console.WriteLine("No location for this book");
                Console.WriteLine("Authors:");
                foreach (AuthorsBooksEntity ab in bookDetailed.AuthorsBooks)
                {
                    Console.WriteLine($"{ab.Author.FirstName} {ab.Author.LastName}");
                }
                Console.WriteLine("Genres:");
                foreach (GenresBooksEntity gb in bookDetailed.GenresBooks)
                {
                    Console.WriteLine(gb.Genre.Name);
                }
                Console.WriteLine();
            }

            var locationRepo = new LocationRepository(context);
            Console.WriteLine("\nAll shelves:");
            foreach (LocationEntity location in locationRepo.Get().ToList())
            {
                LocationEntity locDetailed = locationRepo.GetLocationDetailed(location.Id);
                Console.WriteLine($"Place: {locDetailed.Place}\t\tShelf: {locDetailed.Shelf}\nBooks are:");
                foreach (BookEntity book in locDetailed.Books)
                {
                    Console.WriteLine($"Name: {book.Name}\t\tAnnotation: {book.Annotation}");
                }
                Console.WriteLine();
            }

            var authorRepo = new AuthorRepository(context);
            Console.WriteLine("\nAll authors:");
            foreach (AuthorEntity author in authorRepo.Get().ToList())
            {
                AuthorEntity authorDetailed = authorRepo.GetAuthorDetailed(author.Id);
                Console.WriteLine($"{authorDetailed.FirstName} {authorDetailed.LastName}\nBooks of this author:");
                foreach (AuthorsBooksEntity ab in authorDetailed.AuthorsBooks)
                {
                    Console.WriteLine($"Name: {ab.Book.Name}\nAnnotation: {ab.Book.Annotation}");
                }
                Console.WriteLine();
            }

            var genreRepo = new GenreRepository(context);
            Console.WriteLine("\nAll genres:");
            foreach (var genre in genreRepo.Get().ToList())
            {
                GenreEntity genreDetailed = genreRepo.GetGenreDetailed(genre.Id);
                Console.WriteLine($"{genreDetailed.Name}\nBooks of this genre:");
                foreach (GenresBooksEntity gb in genreDetailed.GenresBooks)
                {
                    Console.WriteLine($"Name: {gb.Book.Name}\nAnnotation: {gb.Book.Annotation}");
                }
                Console.WriteLine();
            }
        }

        private static void TestUpdating(AppContext context)
        {
            Console.WriteLine("Test Updating");
            var bookRepo = new BookRepository(context);
            var locationRepo = new LocationRepository(context);
            var book = context.Books.AsNoTracking().FirstOrDefault();
            //BUG! Related data are not updated
            try
            {
                var newBook = new BookEntity { Id = book.Id, Name = "New book", Annotation = "New Annotation", Location = context.Locations.FirstOrDefault() };
                bookRepo.Update(newBook);
                Console.WriteLine(newBook.Location.Place);
                var updatedBook = bookRepo.GetBookDetailed(book.Id);
                Console.WriteLine(updatedBook.Name);
                //Console.WriteLine(updatedBook.Location.Place);
                Console.WriteLine("Successful update");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TestCreating(AppContext context)
        {
            Console.WriteLine("Test creating");
            var bookRepo = new BookRepository(context);
            var newBook = new BookEntity { Name = "New book", Annotation = "New Annotation", Location = context.Locations.FirstOrDefault() };
            bookRepo.Create(newBook);
            TestReading(context);
        }

        private static AppContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Data.AppContext>();
            string connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;App=EntityFramework;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            return new AppContext(optionsBuilder.Options);
        }
    }
}
