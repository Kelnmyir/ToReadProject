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

        public DbSet<Book> Books { get; set; }
    }
}
