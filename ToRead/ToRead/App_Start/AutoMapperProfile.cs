using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ToRead.MVC
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Models.Book, MVC.Models.BookModel>();
            CreateMap<MVC.Models.BookModel, Data.Models.Book>();

            CreateMap<Data.Models.Location, MVC.Models.LocationModel>();
            CreateMap<MVC.Models.LocationModel, Data.Models.Location>();

        }
    }
}
