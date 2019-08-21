using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ToRead.Data
{
    interface IRepository<T> where T: class
    {
        IQueryable<T> Get();

        void Create(T obj);

        void Update(T obj);

        void Delete(T obj);
    }
}
