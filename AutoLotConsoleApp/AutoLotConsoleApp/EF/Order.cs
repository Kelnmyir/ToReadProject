namespace AutoLotConsoleApp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderID { get; set; }

        public int CustID { get; set; }

        public int CarID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Car Car { get; set; }
    }
}
