using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("genres")]
    public class GenreEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GenresBooksEntity> GenresBooks { get; } = new List<GenresBooksEntity>();
    }
}
