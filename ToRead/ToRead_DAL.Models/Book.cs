using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ToRead_DAL.Models.Base;

namespace ToRead_DAL.Models
{
    public class Book : EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string PictureLink { get; set; }

        [StringLength(500)]
        public string Annotation { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }
    }
}
