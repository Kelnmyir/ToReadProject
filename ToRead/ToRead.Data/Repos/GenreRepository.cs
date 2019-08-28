using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;

namespace ToRead.Data
{
    public class GenreRepository : Repository<GenreEntity>, IGenreRepository
    {
        public GenreRepository(AppContext context) : base(context)
        {

        }
        public GenreEntity GetGenreDetailed(int id)
        {
            var genre = _context.Genres
                .Include(g => g.GenresBooks)
                    .ThenInclude(gb => gb.Book)
                .Where(g => g.Id == id)
                .Single();
            return genre;
        }
    }
}
