using BeSpoked.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Models
{
    public class DiscountCreateViewModel
    {
        public DiscountCreateViewModel()
        {
            this.Products = new List<SelectListItem>();
        }
        public string SelectedProduct { get; set; }
        public Discount Discount{ get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
