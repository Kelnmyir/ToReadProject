using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToRead.Data.Models;

namespace ToRead.Data
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AppContext context) : base(context)
        {

        }

        public Location GetLocationDetailed(int id)
        {
            var loc = _context.Locations
                .Include(l => l.Books)
                .Where(l => l.Id == id)
                .Single();
            return loc;
        }
    }
}
