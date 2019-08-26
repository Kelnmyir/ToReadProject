using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;


namespace ToRead.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppContext context) : base (context)
        {

        }

        public Book GetBookDetailed(int id)
        {
            var book = _context.Books
                .Include(b => b.Location)
                .Where(b => b.Id == id)
                .Single();
            return book;
        }
    }
}
