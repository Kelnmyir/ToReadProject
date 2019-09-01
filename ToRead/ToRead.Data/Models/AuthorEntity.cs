using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("authors")]
    public class AuthorEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<AuthorsBooksEntity> AuthorsBooks { get; set; } = new List<AuthorsBooksEntity>();
    }
}
