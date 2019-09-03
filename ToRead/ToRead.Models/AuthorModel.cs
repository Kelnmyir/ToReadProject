using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToRead.MVC.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<BookModel> BookModels { get; set; } = new List<BookModel>();
    }
}
