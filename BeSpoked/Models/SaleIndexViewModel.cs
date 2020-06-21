using BeSpoked.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Models
{
    public class SaleIndexViewModel
    {
        public SaleIndexViewModel()
        {
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
    }
}
