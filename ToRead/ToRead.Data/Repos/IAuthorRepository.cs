using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public interface IAuthorRepository : IRepository<AuthorEntity>
    {
        AuthorEntity GetAuthorDetailed(int id);
    }
}
