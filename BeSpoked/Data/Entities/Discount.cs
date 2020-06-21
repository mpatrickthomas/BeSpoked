using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Data.Entities
{
    public class Discount
    {
        public int id { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Begin Date")]
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
