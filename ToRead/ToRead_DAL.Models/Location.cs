using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ToRead_DAL.Models.Base;

namespace ToRead_DAL.Models
{
    public class Location : EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}
