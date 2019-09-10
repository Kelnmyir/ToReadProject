using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ToRead.Data.EF
{
    public class AuthorRepository : Repository<AuthorEntity>, IAuthorRepository
    {
        public AuthorRepository(AppContext context) : base(context)
        {

        }
        public AuthorEntity GetAuthorDetailed(int id)
        {
            var author = _context.Authors
                .Include(a => a.AuthorsBooks)
                    .ThenInclude(ab => ab.Book)
                .Where(a => a.Id == id)
                .Single();
            return author;
        }
    }
}
