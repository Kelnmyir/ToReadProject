using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using ToRead_DAL.Models.Base;

namespace ToRead_DAL.Repos
{
    public class Repo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly ToReadContext _db;
        protected ToReadContext Context => _db;

        public Repo() : this(new ToReadContext())
        {

        }
        public Repo(ToReadContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }

        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }

        public int Delete(int id, byte[] timeStamp)
        {
            _db.Entry(new T() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public virtual T GetOne(int? id)
        {
            return _table.Find(id);
        }

        public List<T> GetSome(Expression<Func<T, bool>> where)
        {
            return _table.Where(where).ToList();
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
        {
            return (ascending ? _table.OrderBy(orderBy) : _table.OrderByDescending(orderBy)).ToList();
        }

        public List<T> ExecuteQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
