using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public interface IRepository<T> where T: BaseEntity
    {
        IQueryable<T> Get();

        T GetOne(int id);

        void Create(T obj);

        void Update(T obj);

        void Delete(T obj);
    }
}
