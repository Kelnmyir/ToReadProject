using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class AuthorsBooksRepository : IAuthorsBooksRepository
    {
        protected readonly SqlConnection _connection;
        protected readonly string _connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;Connection Timeout=30;";

        public AuthorsBooksRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }
        public void Create(ICollection<AuthorsBooksEntity> abs)
        {
            string entries = "";
            foreach (var ab in abs)
            {
                entries += $"({ab.Author.Id}, {ab.Book.Id}), ";
            }
            string sql = $@"INSERT INTO authorsBooks (AuthorId, BookId)
                VALUES {entries.Trim(new char[] { ',', ' ' })}";
            SqlCommand cmd = new SqlCommand(sql, _connection);

            _connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception("AuthorsBooksEntity is not created");
            }
            _connection.Close();
        }

        public void Create(AuthorsBooksEntity ab)
        {
            var abs = new List<AuthorsBooksEntity> { ab };
            this.Create(abs);
        }

        public void Delete(AuthorsBooksEntity ab)
        {
            string sql = $"DELETE FROM authorsBooks WHERE (AuthorId = {ab.AuthorId}) AND (BookId = {ab.BookId})";
            SqlCommand cmd = new SqlCommand(sql, _connection);
            _connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception("AuthorsBooksEntity is not deleted");
            }
            _connection.Close();
        }

        public IQueryable<AuthorsBooksEntity> Get()
        {
            string query = "SELECT * FROM authorsBooks";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            var reader = cmd.ExecuteReader();
            List<AuthorsBooksEntity> result = new List<AuthorsBooksEntity>();
            while (reader.Read())
            {
                var ab = new AuthorsBooksEntity
                {
                    AuthorId = (int)reader["AuthorId"],
                    BookId = (int)reader["BookId"]
                };
                result.Add(ab);
            }

            _connection.Close();
            return result.AsQueryable();
        }
    }
}
