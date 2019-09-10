using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;


namespace ToRead.Data.EF
{
    public class BookRepository : Repository<BookEntity>, IBookRepository
    {
        public BookRepository(AppContext context) : base (context)
        {

        }

        public BookEntity GetBookDetailed(int id)
        {
            var book = _context.Books
                .Include(b => b.GenresBooks)
                    .ThenInclude(gb => gb.Genre)
                .Include(b => b.AuthorsBooks)
                    .ThenInclude(ab => ab.Author)
                .Include(b => b.Location)
                .Where(b => b.Id == id)
                .Single();
            return book;
        }
    }
}
