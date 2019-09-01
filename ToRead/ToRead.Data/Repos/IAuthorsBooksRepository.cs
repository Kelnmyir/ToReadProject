using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public interface IAuthorsBooksRepository
    {
        IQueryable<AuthorsBooksEntity> Get();

        void Create(ICollection<AuthorsBooksEntity> abs);

        void Create(AuthorsBooksEntity ab);

        void Delete(AuthorsBooksEntity ab);
    }
}
