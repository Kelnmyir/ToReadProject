using System;
using System.Collections.Generic;
using System.Text;
using ToRead.MVC.Models;

namespace ToRead.Services
{
    public interface IAuthorService
    {
        ICollection<AuthorModel> GetAllAuthors();

        AuthorModel GetAuthor(int id);

        void Create(AuthorModel model);

        void Delete(AuthorModel model);

        void Update(AuthorModel model);
    }
}
