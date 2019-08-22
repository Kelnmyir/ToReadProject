using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Annotation { get; set; }
    }
}
