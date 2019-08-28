using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public interface IGenreRepository : IRepository<GenreEntity>
    {
        GenreEntity GetGenreDetailed(int id);
    }
}
