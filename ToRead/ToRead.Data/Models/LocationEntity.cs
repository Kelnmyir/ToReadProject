using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("locations")]
    public class LocationEntity : BaseEntity
    {
        public string Place { get; set; }

        public int Shelf { get; set; }

        public ICollection<BookEntity> Books { get; } = new List<BookEntity>();
    }
}
