using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToRead.MVC.Models
{
    public class LocationModel
    {
        public int Id { get; set; }

        public string Place { get; set; }

        public int Shelf { get; set; }

        public ICollection<BookModel> BookModels { get; set; } = new List<BookModel>();
    }
}
