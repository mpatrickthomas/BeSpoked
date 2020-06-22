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
        public Salesperson Salesperson{ get; set; }
        public ComissionReportViewModel ComissionReportViewModel { get; set; }
    }
}
