using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ToRead.Data.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
