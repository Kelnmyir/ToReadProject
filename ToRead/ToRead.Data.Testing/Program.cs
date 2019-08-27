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
using Microsoft.EntityFrameworkCore.SqlServer;


namespace ToRead.Data.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = CreateContext();

            var bookRepo = new BookRepository(context);
            Console.WriteLine("\nAll books:");
            foreach (Book book in bookRepo.Get())
            {
                Book bookDetailed = bookRepo.GetBookDetailed(book.Id);
                Console.WriteLine($"Name: {bookDetailed.Name}\nAnnotation: {bookDetailed.Annotation}\nPlace: {bookDetailed.Location.Place}\nShelf: {bookDetailed.Location.Shelf}\n\n");
            }

            var locationRepo = new LocationRepository(context);
            Console.WriteLine("\nAll shelves:");
            foreach (Location location in locationRepo.Get().ToList())
            {
                Location locDetailed = locationRepo.GetLocationDetailed(location.Id);
                Console.WriteLine($"Place: {locDetailed.Place}\t\tShelf: {locDetailed.Shelf}\nBooks are:\n");
                foreach (Book book in locDetailed.Books)
                {
                    Console.WriteLine($"Name: {book.Name}\t\tAnnotation: {book.Annotation}\n\n");
                }
            }

            Console.ReadKey();
        }

        static AppContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Data.AppContext>();
            string connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;App=EntityFramework;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            return new AppContext(optionsBuilder.Options);
        }
    }
}
