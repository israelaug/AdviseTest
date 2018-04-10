using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdviseTest.Models
{
    public class Product_min
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public System.DateTime SellStartDate { get; set; }
        public Nullable<System.DateTime> SellEndDate { get; set; }
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }

    }
}