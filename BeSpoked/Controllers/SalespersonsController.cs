using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeSpoked.Data;
using BeSpoked.Data.Entities;
using BeSpoked.Models;
using static BeSpoked.Models.SalespersonDetailsViewModel;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.Runtime.InteropServices.ComTypes;

namespace BeSpoked.Controllers
{
    public class SalespersonsController : Controller
    {
        private readonly BeSpokedContext _context;

        public SalespersonsController(BeSpokedContext context) => _context = context;

        // GET: Salespersons
        public async Task<IActionResult> Index() => View(await _context.Salespeople.OrderBy(s => s.LastName).ToListAsync());

        // GET: Salespersons/Details/5
        public async Task<IActionResult> Details(int? id, int? quarter, DateTime? year)
        {
            if (id == null) return NotFound();

            var salesperson = await _context.Salespeople.FirstOrDefaultAsync(m => m.id == id);
            if (salesperson == null) return NotFound();

            var sales = _context.Sales
                                    .Include(s => s.Product)
                                    .Include(s => s.Customer)
                                    .Include(s => s.Salesperson)
                                    .ToList()
                                    .Where(
                                            s => s.Salesperson.id == salesperson.id
                                            && s.SaleDate >= new DateTime(DateTime.Now.Year, 1, 1)
                                            && s.SaleDate < new DateTime(DateTime.Now.Year, 3, 1)
                                    );

            var viewmodel = new SalespersonDetailsViewModel
            {
                Salesperson = salesperson,
                ComissionReportViewModel = new ComissionReportViewModel()
                {
                    SelectedQuarter = 1.ToString(), // Default to Q1 current year
                    SelectedYear = DateTime.Now.Year.ToString(),
                    Sales = sales
                }
            };

            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Details(SalespersonDetailsViewModel viewmodel)
        {
            viewmodel.Salesperson = _context.Salespeople.FirstOrDefault(s => s.id == viewmodel.Salesperson.id);

            DateTime date = DateTime.Now;
            int quarter = 1;

            DateTime.TryParse($"1/1/{viewmodel.ComissionReportViewModel.SelectedYear}", out date);
            int.TryParse(viewmodel.ComissionReportViewModel.SelectedQuarter, out quarter);

            /*
            Q1 - Jan - Mar
            Q2 - Apr - Jun
            Q3 - Jul - Sep
            Q4 - Oct - Dec
             */
            DateTime startDate = new DateTime(date.Year, ((quarter - 1) * 3) + 1, 1),
                endDate = new DateTime(date.Year, Math.Min((quarter * 3) + 1, 12), 1);


            var sales = _context.Sales.Include(s => s.Product)
                                    .Include(s => s.Customer)
                                    .Include(s => s.Salesperson)
                                    .ToList()
                                    .Where(
                                            s => s.Salesperson.id == viewmodel.Salesperson.id
                                            && s.SaleDate >= startDate
                                            && s.SaleDate < endDate
                                    );
            viewmodel.ComissionReportViewModel.Sales = sales;

            return View(viewmodel);
        }

        // GET: Salespersons/Create
        public IActionResult Create() => View();

        // POST: Salespersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Address,PhoneNumber,StartDate,TerminationDate,Manager")] Salesperson salesperson)
        {
            if (this.ModelState.IsValid)
            {
                if (_context.Salespeople.Any(s => s.FirstName == salesperson.FirstName.Trim() && s.LastName == salesperson.LastName.Trim()))
                {
                    this.ViewBag.ErrorMessage = "This salesperson already exists in the system";
                    return View(salesperson);
                };

                _context.Add(salesperson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesperson);
        }

        // GET: Salespersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var salesperson = await _context.Salespeople.FindAsync(id);
            if (salesperson == null) return NotFound();
            return View(salesperson);
        }

        // POST: Salespersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Address,PhoneNumber,StartDate,TerminationDate,Manager")] Salesperson salesperson)
        {
            if (id != salesperson.id) return NotFound();

            if (this.ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalespersonExists(salesperson.id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salesperson);
        }

        // GET: Salespersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var salesperson = await _context.Salespeople.FirstOrDefaultAsync(m => m.id == id);
            if (salesperson == null) return NotFound();

            return View(salesperson);
        }

        // POST: Salespersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesperson = await _context.Salespeople.FindAsync(id);
            _context.Salespeople.Remove(salesperson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalespersonExists(int id) => _context.Salespeople.Any(e => e.id == id);
    }
}
