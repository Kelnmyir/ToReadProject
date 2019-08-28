using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public class AppContext : DbContext
    {
        public AppContext (DbContextOptions options) : base(options)
        {

        }

        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<AuthorsBooksEntity> AuthorsBooks { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<GenresBooksEntity> GenresBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorsBooksEntity>().HasKey(ab => new { ab.AuthorId, ab.BookId });
            modelBuilder.Entity<GenresBooksEntity>().HasKey(gb => new { gb.BookId, gb.GenreId });
        }
    }
}
