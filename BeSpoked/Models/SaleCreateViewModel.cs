using BeSpoked.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Models
{
    public class SaleCreateViewModel
    {
        public Sale Sale { get; set; }

        [Required]
        public string SelectedCustomer { get; set; }

        [Required]
        public string SelectedProduct { get; set; }

        [Required]
        public string SelectedSalesperson { get; set; }

        [Display(Name = "Customer")]
        public IEnumerable<SelectListItem> Customers { get; set; }

        [Display(Name = "Product")]
        public IEnumerable<SelectListItem> Products { get; set; }

        [Display(Name = "Salesperson")]
        public IEnumerable<SelectListItem> Salespeople { get; set; }

        public SaleCreateViewModel()
        {
            this.Customers = new List<SelectListItem>();
            this.Products = new List<SelectListItem>();
            this.Salespeople = new List<SelectListItem>();
        }
    }
}
