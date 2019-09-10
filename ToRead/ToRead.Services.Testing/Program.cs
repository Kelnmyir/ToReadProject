using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data;
using ToRead.Data.EF;
using ToRead.Data.Models;
using ToRead.Services;
using ToRead.MVC.Models;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer;
using AppContext = ToRead.Data.EF.AppContext;

namespace ToRead.Services.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = CreateContext();

            var initializer = new DataInitializer(context);
            initializer.Initialize();

            var authorRepo = new AuthorRepository(context);
            var bookRepo = new BookRepository(context);
            var genreRepo = new GenreRepository(context);
            var locationRepo = new LocationRepository(context);
            var abRepo = new AuthorsBooksRepository(context);
            var gbRepo = new GenresBooksRepository(context);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = mapperConfig.CreateMapper();

            Console.WriteLine("Testing books service");
            var bookService = new BookService(bookRepo, authorRepo, abRepo, genreRepo, gbRepo, locationRepo, mapper);
            var authorService = new AuthorService(authorRepo, abRepo, bookRepo, mapper);
            var genreService = new GenreService(bookRepo, gbRepo, genreRepo, mapper);

            GenreTester genreTester = new GenreTester(bookService, genreService);
            genreTester.TestUpdating();

            Console.ReadKey();
        }

        private static AppContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            string connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;App=EntityFramework;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            return new AppContext(optionsBuilder.Options);
        }

    }
}
