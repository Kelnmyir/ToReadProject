using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToRead.Data
{
    public class DataInitializer
    {
        private readonly AppContext _context;

        public DataInitializer(AppContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            ClearTable("books");
            ClearTable("locations");
            RefillLocationTable();
            RefillBooksTable();
        }

        private void ClearTable(string tableName)
        {
            var rawSqlString = $"DELETE FROM dbo.{tableName}";
            _context.Database.ExecuteSqlCommand(rawSqlString);
        }

        private void RefillBooksTable()
        {
            List<Location> locations = _context.Set<Location>().ToList();

            _context.Books.Add(new Book {
                Name = "Collapse",
                Annotation = "How civilizations die or survive",
                Location = locations[0]
            });
            _context.Books.Add(new Book {
                Name = "Pro C# 7",
                Annotation = "You know why you need it",
                Location = locations[1]
            });
            _context.Books.Add(new Book {
                Name = "О мышах и людях",
                Annotation = "Записки экспериментатора",
                Location = locations[0]
            });
            _context.SaveChanges();
        }

        private void RefillLocationTable()
        {
            _context.Locations.AddRange(new List<Location>
            {
                new Location { Place = "Wardrobe", Shelf = 1 },
                new Location { Place = "Wardrobe", Shelf = 2 },
                new Location { Place = "Workplace" }
            });
            _context.SaveChanges();
        }
    }
}
