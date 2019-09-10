using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ToRead.Data.EF
{
    class AppContextFactory : IDesignTimeDbContextFactory<AppContext>
    {
        public AppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            string connectionString = "server=(LocalDb)\\MSSQLLocalDB;database=ToRead;User ID=Kelnmyir;Password=solresol;MultipleActiveResultSets=True;App=EntityFramework;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
            return new AppContext(optionsBuilder.Options);
        }
    }
}
