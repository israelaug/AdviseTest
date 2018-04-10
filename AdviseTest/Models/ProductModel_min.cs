using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdviseTest.Models
{
    public class ProductModel_min
    {

        public int? ProductModelID { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public string Instructions { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

    }
}