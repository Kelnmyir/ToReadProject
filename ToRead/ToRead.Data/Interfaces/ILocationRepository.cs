using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public interface ILocationRepository : IRepository<LocationEntity>
    {
        LocationEntity GetLocationDetailed(int id);
    }
}
