﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToRead.MVC.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Annotation { get; set; }

        public string LocationPlace { get; set; }

        public int LocationShelf { get; set; }

        public ICollection<AuthorModel> AuthorModels { get; set; } = new List<AuthorModel>();

        public ICollection<GenreModel> GenreModels { get; set; } = new List<GenreModel>();
    }
}
