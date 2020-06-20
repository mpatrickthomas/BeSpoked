using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Data.Entities
{
    public class Sale
    {
        public int id { get; set; }
        public Product Product { get; set; }
        public Salesperson Salesperson { get; set; }
        public Customer Customer { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
