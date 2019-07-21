using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotConsoleApp.Models
{
    class ShortCar
    {
        public int CarID { get; set; }
        public string Make { get; set; }
        public override string ToString()
        {
            return $"{this.Make} with ID {this.CarID}.";
        }
    }
}
