using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("genresBooks")]
    public class GenresBooksEntity
    {
        public int BookId { get; set; }
        public BookEntity Book { get; set; }

        public int GenreId { get; set; }
        public GenreEntity Genre { get; set; }
    }
}
