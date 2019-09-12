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
        private readonly AppContext _context;

        public GenresBooksRepository(AppContext context)
        {
            _context = context;
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
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception("GenresBooksEntity is not created");
            }
            _context.CloseConnection();
        }

        public void Create(GenresBooksEntity gb)
        {
            var gbs = new List<GenresBooksEntity> { gb };
            this.Create(gbs);
        }

        public void Delete(GenresBooksEntity gb)
        {
            string sql = $"DELETE FROM genresBooks WHERE (GenreId = {gb.GenreId}) AND (BookId = {gb.BookId})";
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception("GenresBooksEntity is not deleted");
            }
            _context.CloseConnection();
        }

        public IQueryable<GenresBooksEntity> Get()
        {
            string query = "SELECT * FROM genresBooks";
            var reader = _context.StartReader(query);
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

            _context.CloseConnection();
            return result.AsQueryable();
        }
    }
}
