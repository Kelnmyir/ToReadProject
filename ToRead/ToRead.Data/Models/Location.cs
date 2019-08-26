using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("locations")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Place { get; set; }

        public int Shelf { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
