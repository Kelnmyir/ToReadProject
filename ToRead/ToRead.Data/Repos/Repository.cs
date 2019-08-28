using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppContext _context;

        public Repository(AppContext context)
        {
            _context = context;
        }
        public void Create(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }

        public IQueryable<T> Get()
        {
            var result = _context.Set<T>().AsNoTracking();
            return result;
        }

        public T GetOne(int id)
        {
            T result = _context.Set<T>().Find(id);
            return result;
        }

        public virtual void Update(T obj)
        {
            var entityToUpdate = _context.Set<T>().Find(obj.Id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(obj);
            _context.SaveChanges();
        }
    }
}
