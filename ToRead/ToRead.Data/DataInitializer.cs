using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;

namespace ToRead.Data
{
    class DataInitializer
    {
        private readonly AppContext _context;

        public DataInitializer(AppContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            RefillBooksTable();
        }

        private void RefillBooksTable()
        {
            foreach (Book book in _context.Books)
            {
                _context.Books.Remove(book);
            }
            _context.SaveChanges();

            _context.Books.Add(new Book() { Name = "Collapse", Annotation = "How civilizations die or survive" });
            _context.Books.Add(new Book() { Name = "Pro C# 7", Annotation = "You know why you need it" });
            _context.Books.Add(new Book() { Name = "О мышах и людях", Annotation = "Записки экспериментатора" });
            _context.SaveChanges();
        }
    }
}
