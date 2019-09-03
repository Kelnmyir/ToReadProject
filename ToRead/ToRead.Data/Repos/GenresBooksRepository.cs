using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ToRead.Data
{
    public class GenresBooksRepository : IGenresBooksRepository
    {
        private readonly AppContext _context;

        public GenresBooksRepository(AppContext context)
        {
            _context = context;
        }

        public void Create(ICollection<GenresBooksEntity> gbs)
        {
            _context.GenresBooks.AddRange(gbs);
            _context.SaveChanges();
        }

        public void Create(GenresBooksEntity gb)
        {
            _context.GenresBooks.Add(gb);
            _context.SaveChanges();
        }

        public void Delete(GenresBooksEntity gb)
        {
            _context.GenresBooks.Remove(gb);
            _context.SaveChanges();
        }

        public IQueryable<GenresBooksEntity> Get()
        {
            var result = _context.GenresBooks.ToList();
            return result.AsQueryable();
        }
    }
}
