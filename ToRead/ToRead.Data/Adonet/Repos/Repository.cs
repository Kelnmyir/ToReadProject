using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly Dictionary<Type, string> _tableNames = new Dictionary<Type, string>
        {
            {typeof(AuthorEntity), "authors" },
            {typeof(AuthorsBooksEntity), "authorsBooks" },
            {typeof(BookEntity), "books" },
            {typeof(GenreEntity), "genres" },
            {typeof(GenresBooksEntity), "genresBooks" },
            {typeof(LocationEntity), "locations" }
        };
        protected readonly AppContext _context;

        public Repository(AppContext context)
        {
            _context = context;
        }

        public void Create(T obj)
        {
            string columns = "";
            string values = "";
            foreach (var property in typeof(T).GetProperties())
            {
                if ((property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string)))
                    && (property.GetValue(obj) != null) && (!property.Name.Equals("Id")))
                {
                    columns += property.Name + ", ";
                    values +=$"'{property.GetValue(obj)}', ";
                }
                if (property.PropertyType.IsSubclassOf(typeof(BaseEntity)))
                {
                    columns += $"{property.Name}Id, ";
                    if (property.GetValue(obj) == null)
                    {
                        values += $"NULL, ";
                    }
                    else
                    {
                        BaseEntity relatedEntity = (BaseEntity)property.GetValue(obj);
                        values += $"{relatedEntity.Id}, ";
                    }
                }
            }
            columns = columns.Trim(new char[] { ',', ' ' });
            values = values.Trim(new char[] { ',', ' ' });

            string sql = $@"INSERT INTO {_tableNames[typeof(T)]} ({columns})
                VALUES ({values})";
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception($"{typeof(T).Name} is not created");
            }

            sql = "SELECT SCOPE_IDENTITY() AS Id";
            obj.Id = _context.ExecuteScalar(sql);
            _context.CloseConnection();
        }

        public void Delete(T obj)
        {
            string sql = $@"DELETE FROM {_tableNames[typeof(T)]} WHERE Id = {obj.Id}";
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception($"{typeof(T).Name} is not created");
            }
            _context.CloseConnection();
        }

        public IQueryable<T> Get()
        {
            string query = $"SELECT * FROM {_tableNames[typeof(T)]}";
            var reader = _context.StartReader(query);

            var result = new List<T>();
            while (reader.Read())
            {
                T entity = new T();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string))
                        && !Convert.IsDBNull(reader[property.Name]))
                    {
                        property.SetValue(entity, reader[property.Name]);
                    }
                }
                result.Add(entity);
            }

            _context.CloseConnection();
            return result.AsQueryable();
        }

        public T GetOne(int id)
        {
            string query = $"SELECT * FROM {_tableNames[typeof(T)]} WHERE Id = {id}";
            var reader = _context.StartReader(query);

            T result = new T();
            if (reader.HasRows)
            {
                reader.Read();
                foreach (var property in typeof(T).GetProperties())
                {
                    if ((property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string)))
                        && !Convert.IsDBNull(reader[property.Name]))
                    {
                        property.SetValue(result, reader[property.Name]);
                    }
                }
            }
            else
            {
                _context.CloseConnection();
                throw (new Exception("Entity not found"));
            }

            _context.CloseConnection();
            return result;
        }

        public void Update(T obj)
        {
            string columnsAndValues = "";
            foreach (var property in typeof(T).GetProperties())
            {
                if ((property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string)))
                    && (property.GetValue(obj) != null) && (!property.Name.Equals("Id")))
                {
                    columnsAndValues += $@"{property.Name} = '{property.GetValue(obj)}', ";
                }
                if (property.GetType().IsSubclassOf(typeof(BaseEntity)))
                {
                    if (property.GetValue(obj) == null)
                        columnsAndValues += $@"{property.Name}Id = NULL, ";
                    else
                        columnsAndValues += $@"{property.Name}Id = {obj.Id}, ";
                }
            }
            columnsAndValues = columnsAndValues.Trim(new char[] { ',',' '});

            string sql = $@"UPDATE {_tableNames[typeof(T)]} SET {columnsAndValues} WHERE Id = {obj.Id}";
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception($"{typeof(T).Name} is not created");
            }
            _context.CloseConnection();
        }
    }
}
