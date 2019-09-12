using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public interface IGenresBooksRepository
    {
        IQueryable<GenresBooksEntity> Get();

        void Create(ICollection<GenresBooksEntity> gbs);

        void Create(GenresBooksEntity gb);

        void Delete(GenresBooksEntity gb);
    }
}
