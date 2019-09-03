using System;
using System.Collections.Generic;
using System.Text;
using ToRead.MVC.Models;

namespace ToRead.Services
{
    public interface IGenreService
    {
        ICollection<GenreModel> GetAllGenres();

        GenreModel GetGenre(int id);

        void Create(GenreModel model);

        void Delete(GenreModel model);

        void Update(GenreModel model);
    }
}
