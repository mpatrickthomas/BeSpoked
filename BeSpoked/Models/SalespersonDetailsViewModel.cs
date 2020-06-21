using BeSpoked.Data.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Models
{
    public class SalespersonDetailsViewModel
    {
        public SalespersonDetailsViewModel()
        {
            Quarters = new List<SelectListItem>();
        }

        public Salesperson Salesperson{ get; set; }
        public IEnumerable<Sale> Sales { get; set; }
        public string SelectedQuarter { get; set; }

        [Display(Name ="Selected Quarter")]
        public IEnumerable<SelectListItem> Quarters { get; set; }

        public enum FiscalQuarters
        {
            Q1, Q2, Q3, Q4
        }
    }
}
