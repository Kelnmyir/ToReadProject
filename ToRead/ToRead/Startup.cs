using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer;
using ToRead.Data;
using ToRead.Data.EF;
using ToRead.Data.Adonet;
using ToRead.Data.Models;
using ToRead.Services;



namespace ToRead
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToRead.Data.EF.AppContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("local")));

            switch (Configuration.GetValue<string>("DataProvider"))
            {
                case "EF":
                    services.AddScoped<IBookRepository, Data.EF.BookRepository>();
                    services.AddScoped<ILocationRepository, Data.EF.LocationRepository>();
                    services.AddScoped<IAuthorRepository, Data.EF.AuthorRepository>();
                    services.AddScoped<IGenreRepository, Data.EF.GenreRepository>();
                    break;
                case "ADONET":
                    services.AddScoped<IBookRepository, Data.Adonet.BookRepository>();
                    services.AddScoped<ILocationRepository, Data.Adonet.LocationRepository>();
                    services.AddScoped<IAuthorRepository, Data.Adonet.AuthorRepository>();
                    services.AddScoped<IGenreRepository, Data.Adonet.GenreRepository>();
                    break;
                default:
                    throw new Exception("Config file is broken");
            }

            services.AddMvc();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
