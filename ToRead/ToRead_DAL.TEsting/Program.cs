using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using ToRead_DAL;
using ToRead_DAL.DataInitialization;
using ToRead_DAL.Repos;
using ToRead_DAL.Models;


namespace ToRead_DAL.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing DB");
            using (var context=new ToReadContext())
            {
                MyDataInitializer.RecreateDatabase(context);
                MyDataInitializer.InitializeData(context);
                Console.WriteLine("Using EF");
                Console.WriteLine("Books are:");
                foreach (Book book in context.Books
                    .Include(x=>x.Location)
                    .Include(x=>x.AuthorBooks)
                    .ThenInclude(x=>x.Author))
                {
                    Console.WriteLine(book.Name);
                    if (book.AuthorBooks != null)
                    {
                        Console.WriteLine("  Related autors:");
                        var authors = book.AuthorBooks.Select(ab => ab.Author);
                        foreach (Author author in authors)
                        {
                            Console.WriteLine($"  {author.FirstName} {author.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No related author!");
                    }
                    if (book.Location != null)
                    {
                        var location = book.Location;
                        Console.WriteLine($"  The book is located in {location.Name}\n");
                    }
                    else Console.WriteLine("  No location for this book!\n");
                }
            }
            using (var repo = new BookRepo())
            {
                Console.WriteLine("Using Repo");
                Console.WriteLine("Books are:");
                foreach (Book book in repo.GetAll())
                {
                    var b = repo.GetOne(book.Id);
                    Console.WriteLine(b.Name);

                    if (b.Location == null)
                        Console.WriteLine("  The book is located nowhere!");
                    else
                    {
                        Location location = repo.GetLocation(b);
                        Console.WriteLine($"  The book is located in {location.Name}");
                    }

                    var authors = repo.GetAuthors(b);
                    Console.WriteLine("  Related authors:");
                    foreach (Author a in authors)
                    {
                        Console.WriteLine($"  {a.FirstName} {a.LastName}");
                    }

                    var genres = repo.GetGenres(b);
                    Console.WriteLine("  Related genres:");
                    foreach (var g in genres)
                    {
                        Console.WriteLine($"  {g.Name}");
                    }

                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
