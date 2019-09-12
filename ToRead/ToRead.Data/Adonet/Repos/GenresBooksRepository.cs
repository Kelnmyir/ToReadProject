using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class GenresBooksRepository : IGenresBooksRepository
    {
        protected readonly SqlConnection _connection;
        protected readonly string _connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;Connection Timeout=30;";

        public GenresBooksRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public void Create(ICollection<GenresBooksEntity> gbs)
        {
            string entries = "";
            foreach (var gb in gbs)
            {
                entries += $"({gb.Genre.Id}, {gb.Book.Id}), ";
            }
            string sql = $@"INSERT INTO genresBooks (GenreId, BookId)
                VALUES {entries.Trim(new char[] { ',', ' ' })}";
            SqlCommand cmd = new SqlCommand(sql, _connection);

            _connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception("GenresBooksEntity is not created");
            }
            _connection.Close();
        }

        public void Create(GenresBooksEntity gb)
        {
            var gbs = new List<GenresBooksEntity> { gb };
            this.Create(gbs);
        }

        public void Delete(GenresBooksEntity gb)
        {
            string sql = $"DELETE FROM genresBooks WHERE (GenreId = {gb.GenreId}) AND (BookId = {gb.BookId})";
            SqlCommand cmd = new SqlCommand(sql, _connection);
            _connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                _connection.Close();
                throw new Exception("GenresBooksEntity is not deleted");
            }
            _connection.Close();
        }

        public IQueryable<GenresBooksEntity> Get()
        {
            string query = "SELECT * FROM genresBooks";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            var reader = cmd.ExecuteReader();
            List<GenresBooksEntity> result = new List<GenresBooksEntity>();
            while (reader.Read())
            {
                var gb = new GenresBooksEntity
                {
                    GenreId = (int)reader["GenreId"],
                    BookId = (int)reader["BookId"]
                };
                result.Add(gb);
            }

            _connection.Close();
            return result.AsQueryable();
        }
    }
}
