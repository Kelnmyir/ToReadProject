using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToRead.Data.Models
{
    [Table("authorsBooks")]
    public class AuthorsBooksEntity
    {
        public int BookId { get; set; }
        public BookEntity Book { get; set; }

        public int AuthorId { get; set; }
        public AuthorEntity Author { get; set; }
    }
}
