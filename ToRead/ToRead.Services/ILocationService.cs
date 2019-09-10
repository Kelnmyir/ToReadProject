using System;
using System.Collections.Generic;
using System.Text;
using ToRead.MVC.Models;

namespace ToRead.Services
{
    public interface ILocationService
    {
        ICollection<LocationModel> GetAllLocations();

        LocationModel GetLocation(int id);

        void Create(LocationModel model);

        void Delete(LocationModel model);

        void Update(LocationModel model);
    }
}
