using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ToRead.Services.Testing
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Models.BookEntity, MVC.Models.BookModel>();
            CreateMap<MVC.Models.BookModel, Data.Models.BookEntity>();

            CreateMap<Data.Models.LocationEntity, MVC.Models.LocationModel>();
            CreateMap<MVC.Models.LocationModel, Data.Models.LocationEntity>();

            CreateMap<Data.Models.AuthorEntity, MVC.Models.AuthorModel>();
            CreateMap<MVC.Models.AuthorModel, Data.Models.AuthorEntity>();

            CreateMap<Data.Models.GenreEntity, MVC.Models.GenreModel>();
            CreateMap<MVC.Models.GenreModel, Data.Models.GenreEntity>();
        }
    }
}
