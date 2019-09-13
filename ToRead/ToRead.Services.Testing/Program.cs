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

            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            switch (ConfigurationManager.AppSettings["dataProvider"])
            {
                case "EF":
                    var efcontext = CreateContext();

                    var initializer = new Data.EF.DataInitializer(efcontext);
                    initializer.Initialize();

                    authorRepo = new Data.EF.AuthorRepository(efcontext);
                    bookRepo = new Data.EF.BookRepository(efcontext);
                    genreRepo = new Data.EF.GenreRepository(efcontext);
                    locationRepo = new Data.EF.LocationRepository(efcontext);
                    abRepo = new Data.EF.AuthorsBooksRepository(efcontext);
                    gbRepo = new Data.EF.GenresBooksRepository(efcontext);
                    break;
                case "ADONET":
                    var adonetContext = new Data.Adonet.AppContext("server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;App=EntityFramework;Connection Timeout=30;");

                    var adonetInitialzer = new Data.Adonet.DataInitializer(adonetContext);
                    adonetInitialzer.Initialize();

                    authorRepo = new Data.Adonet.AuthorRepository(adonetContext);
                    bookRepo = new Data.Adonet.BookRepository(adonetContext);
                    genreRepo = new Data.Adonet.GenreRepository(adonetContext);
                    locationRepo = new Data.Adonet.LocationRepository(adonetContext);
                    abRepo = new Data.Adonet.AuthorsBooksRepository(adonetContext);
                    gbRepo = new Data.Adonet.GenresBooksRepository(adonetContext);
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

            BookTester tester = new BookTester(bookService);
            tester.TestUpdating();

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
