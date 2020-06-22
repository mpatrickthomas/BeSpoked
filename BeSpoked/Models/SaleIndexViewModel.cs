using BeSpoked.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Models
{
    public class SaleIndexViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<Sale> Sales { get; set; }

        public SaleIndexViewModel()
        {
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MaxValue;
        }

    }
}
