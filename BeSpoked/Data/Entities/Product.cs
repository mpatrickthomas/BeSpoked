using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Data.Entities
{
    public class Product
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string  Manufacturer { get; set; }

        [Required]
        public string Style { get; set; }

        [Required]
        [Display(Name="Purchase Price")]
        public decimal PurchasePrice{ get; set; }

        [Required]
        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

        [Required]
        [Display(Name = "Current Quantity")]
        public int CurrentQuantity { get; set; }

        [Required]
        [Display(Name = "Comission Percentage")]
        public decimal ComissionPercentage { get; set; }
    }
}
