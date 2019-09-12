using System;
using System.Collections.Generic;
using System.Text;
using ToRead.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;

namespace ToRead.Data.Adonet
{
    public class LocationRepository : Repository<LocationEntity>, ILocationRepository
    {
        public LocationRepository(AppContext context) : base(context) { }

        public LocationEntity GetLocationDetailed(int id)
        {
            var location = this.GetOne(id);

            string bookQuery= $@"SELECT
                    books.Id AS Id,
                    books.Name AS Name,
                    books.Annotation AS Annotation
                FROM locations
                INNER JOIN books
                    ON books.LocationId = locations.Id
                WHERE locations.Id = {location.Id}";
            var bookReader = _context.StartReader(bookQuery);

            while (bookReader.Read())
            {
                var book = new BookEntity();
                foreach (var property in typeof(BookEntity).GetProperties())
                {
                    if (property.PropertyType.IsPrimitive || (property.PropertyType == typeof(string))
                        && (!Convert.IsDBNull(bookReader[property.Name])))
                    {
                        property.SetValue(book, bookReader[property.Name]);
                    }
                }
                location.Books.Add(book);
            }

            _context.CloseConnection();
            return location;
        }
    }
}
