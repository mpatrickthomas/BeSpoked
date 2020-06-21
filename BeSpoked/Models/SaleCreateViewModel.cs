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
        public SaleCreateViewModel()
        {
            Customers = new List<SelectListItem>();
            Products = new List<SelectListItem>();
            Salespeople = new List<SelectListItem>();
        }
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
    }
}
