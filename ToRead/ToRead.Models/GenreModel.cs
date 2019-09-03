using System;
using System.Collections.Generic;
using System.Text;

namespace ToRead.MVC.Models
{
    public class GenreModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<BookModel> BookModels { get; set; } = new List<BookModel>();
    }
}
