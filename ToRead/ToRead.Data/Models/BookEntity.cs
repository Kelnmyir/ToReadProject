using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("books")]
    public class BookEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Annotation { get; set; }

        public LocationEntity Location { get; set; }

        public ICollection<AuthorsBooksEntity> AuthorsBooks { get; } = new List<AuthorsBooksEntity>();

        public ICollection<GenresBooksEntity> GenresBooks { get; } = new List<GenresBooksEntity>();
    }
}
