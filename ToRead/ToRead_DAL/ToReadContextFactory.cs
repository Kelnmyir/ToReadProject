using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ToRead_DAL
{
    class ToReadContextFactory : IDesignTimeDbContextFactory<ToReadContext>
    {
        public ToReadContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ToReadContext>();
            var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=ToRead;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            builder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            return new ToReadContext(builder.Options);
        }
    }
}
