using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ProductViewModel
    {
  
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }


        public string Description { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }
    }
}
