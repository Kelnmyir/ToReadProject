using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data;
using ToRead.Data.EF;
using ToRead.Data.Adonet;
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
using System.Configuration;
using System.Collections.Specialized;

namespace ToRead.Services.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            IAuthorRepository authorRepo;
            IBookRepository bookRepo;
            IGenreRepository genreRepo;
            ILocationRepository locationRepo;
            IAuthorsBooksRepository abRepo;
            IGenresBooksRepository gbRepo;
            switch (ConfigurationManager.AppSettings["dataProvider"])
            {
                case "EF":
                    var context = CreateContext();

                    var initializer = new DataInitializer(context);
                    initializer.Initialize();

                    authorRepo = new Data.EF.AuthorRepository(context);
                    bookRepo = new Data.EF.BookRepository(context);
                    genreRepo = new Data.EF.GenreRepository(context);
                    locationRepo = new Data.EF.LocationRepository(context);
                    abRepo = new Data.EF.AuthorsBooksRepository(context);
                    gbRepo = new Data.EF.GenresBooksRepository(context);
                    break;
                case "ADONET":
                    authorRepo = new Data.Adonet.AuthorRepository();
                    bookRepo = new Data.Adonet.BookRepository();
                    genreRepo = new Data.Adonet.GenreRepository();
                    locationRepo = new Data.Adonet.LocationRepository();
                    abRepo = new Data.Adonet.AuthorsBooksRepository();
                    gbRepo = new Data.Adonet.GenresBooksRepository();
                    break;
                default:
                    throw new Exception("Config file is broken");
            }

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = mapperConfig.CreateMapper();

            Console.WriteLine("Testing services");
            var bookService = new BookService(bookRepo, authorRepo, abRepo, genreRepo, gbRepo, locationRepo, mapper);
            var authorService = new AuthorService(authorRepo, abRepo, bookRepo, mapper);
            var genreService = new GenreService(bookRepo, gbRepo, genreRepo, mapper);
            var locationService = new LocationService(locationRepo, bookRepo, mapper);

            LocationTester tester = new LocationTester(bookService, locationService);
            tester.TestReading();

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
