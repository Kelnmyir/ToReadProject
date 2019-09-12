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
        protected readonly AppContext _context;

        public AuthorsBooksRepository(AppContext context)
        {
            _context = context;
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
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception("AuthorsBooksEntity is not created");
            }
            _context.CloseConnection();
        }

        public void Create(AuthorsBooksEntity ab)
        {
            var abs = new List<AuthorsBooksEntity> { ab };
            this.Create(abs);
        }

        public void Delete(AuthorsBooksEntity ab)
        {
            string sql = $"DELETE FROM authorsBooks WHERE (AuthorId = {ab.AuthorId}) AND (BookId = {ab.BookId})";
            int rowsAffected = _context.StartNonQuery(sql);
            if (rowsAffected <= 0)
            {
                _context.CloseConnection();
                throw new Exception("AuthorsBooksEntity is not deleted");
            }
            _context.CloseConnection();
        }

        public IQueryable<AuthorsBooksEntity> Get()
        {
            string query = "SELECT * FROM authorsBooks";
            var reader = _context.StartReader(query);

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

            _context.CloseConnection();
            return result.AsQueryable();
        }
    }
}
