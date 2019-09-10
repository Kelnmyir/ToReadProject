using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ToRead.Data.EF
{
    public class AuthorsBooksRepository : IAuthorsBooksRepository
    {
        private AppContext _context;

        public AuthorsBooksRepository(AppContext context)
        {
            _context = context;
        }

        public void Create(ICollection<AuthorsBooksEntity> abs)
        {
            _context.AuthorsBooks.AddRange(abs);
            _context.SaveChanges();
        }

        public void Create(AuthorsBooksEntity ab)
        {
            _context.AuthorsBooks.Add(ab);
            _context.SaveChanges();
        }

        public void Delete(AuthorsBooksEntity ab)
        {
            _context.AuthorsBooks.Remove(ab);
            _context.SaveChanges();
        }

        public IQueryable<AuthorsBooksEntity> Get()
        {
            var result = _context.AuthorsBooks.ToList();
            return result.AsQueryable();
        }
    }
}
