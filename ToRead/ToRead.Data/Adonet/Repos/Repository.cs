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
        protected readonly SqlConnection _connection;
        protected readonly string _connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;Connection Timeout=30;";

        public Repository()
        {
            _connection = new SqlConnection(_connectionString);
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
            }
            columns = columns.Trim(new char[] { ',', ' ' });
            values = values.Trim(new char[] { ',', ' ' });
            string sql = $@"INSERT INTO {_tableNames[typeof(T)]} ({columns})
                VALUES ({values})";

            _connection.Open();
            var cmd = new SqlCommand(sql, _connection);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception($"{typeof(T).Name} is not created");
            }

            sql = "SELECT SCOPE_IDENTITY() AS Id";
            cmd = new SqlCommand(sql, _connection);
            obj.Id = Decimal.ToInt32((decimal)cmd.ExecuteScalar());

            _connection.Close();
        }

        public void Delete(T obj)
        {
            string sql = $@"DELETE FROM {_tableNames[typeof(T)]} WHERE Id = {obj.Id}";
            _connection.Open();
            var cmd = new SqlCommand(sql, _connection);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception($"{typeof(T).Name} is not created");
            }
            _connection.Close();
        }

        public IQueryable<T> Get()
        {
            _connection.Open();
            string query = $"SELECT * FROM {_tableNames[typeof(T)]}";
            var cmd = new SqlCommand(query, _connection);
            var reader = cmd.ExecuteReader();

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
            _connection.Close();
            return result.AsQueryable();
        }

        public T GetOne(int id)
        {
            _connection.Open();
            string query = $"SELECT * FROM {_tableNames[typeof(T)]} WHERE Id = {id}";
            var cmd = new SqlCommand(query, _connection);
            var reader = cmd.ExecuteReader();

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
                _connection.Close();
                throw (new Exception("Entity not found"));
            }

            _connection.Close();
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
                if ((property.GetValue(obj) == null) && property.GetType().IsSubclassOf(typeof(BaseEntity)))
                {
                    columnsAndValues += $@"{property.Name}Id = NULL, ";
                }
            }
            Console.WriteLine(columnsAndValues);
            columnsAndValues = columnsAndValues.Trim(new char[] { ',',' '});
            string sql = $@"UPDATE {_tableNames[typeof(T)]} SET {columnsAndValues} WHERE Id = {obj.Id}";

            _connection.Open();
            var cmd = new SqlCommand(sql, _connection);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception($"{typeof(T).Name} is not created");
            }
            _connection.Close();
        }
    }
}
