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
    public class ComissionReportViewModel
    {
        private const int BUSINESS_START_YEAR = 2005;

        public ComissionReportViewModel()
        {
            this.QuarterValues = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = 1.ToString(),
                    Text = $"Q1"
                },
                new SelectListItem
                {
                    Value = 2.ToString(),
                    Text = $"Q2"
                },
                new SelectListItem
                {
                    Value = 3.ToString(),
                    Text = $"Q3"
                },
                new SelectListItem
                {
                    Value = 4.ToString(),
                    Text = $"Q4"
                },
            };

            this.YearValues = new List<SelectListItem>();

            for(int year = BUSINESS_START_YEAR; year <= DateTime.Now.Year; year++)
            {
                this.YearValues.Add(new SelectListItem
                {
                    Value = year.ToString(),
                    Text = year.ToString()
                });
            }
        }

        //public Salesperson Salesperson { get; set; }
        public string SelectedQuarter { get; set; }
        public string SelectedYear { get; set; }

        [Display(Name = "Fiscal Quarter")]
        public IEnumerable<SelectListItem> QuarterValues { get; set; }

        [Display(Name = "Year")]
        public List<SelectListItem> YearValues { get; set; }

        public IEnumerable<Sale> Sales { get; set; }
    }
}
