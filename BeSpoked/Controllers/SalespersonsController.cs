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

namespace BeSpoked.Controllers
{
    public class SalespersonsController : Controller
    {
        private readonly BeSpokedContext _context;

        public SalespersonsController(BeSpokedContext context)
        {
            _context = context;
        }

        // GET: Salespersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salespeople.ToListAsync());
        }

        // GET: Salespersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //else if (quarter == null)
            //{
            //    quarter = 1;
            //}
            //else if( quarter < 1)
            //{
            //    quarter = 1;
            //}else if(quarter > 4)
            //{
            //    quarter = 4;
            //}

            var salesperson = await _context.Salespeople
                .FirstOrDefaultAsync(m => m.id == id);
            if (salesperson == null)
            {
                return NotFound();
            }

            var sales = _context.Sales
                                    .Where(s => s.Salesperson.id == salesperson.id)
                                        //&& s.SaleDate >= new DateTime(DateTime.Now.Year, 1, 1)
                                        //&& s.SaleDate < new DateTime(DateTime.Now.Year, 3, 1))
                                    .Include(s => s.Product)
                                    .Include(s => s.Customer)
                                    .Include(s => s.Salesperson)
                                    .ToList();

            return View(
                new SalespersonDetailsViewModel
                {
                    Salesperson = salesperson,
                    Sales = sales,
                    Quarters = new List<SelectListItem>()
                    {
                        new SelectListItem
                        {
                            Value = FiscalQuarters.Q1.ToString(),
                            Text = $"{FiscalQuarters.Q1} {DateTime.Now.Year}"
                        },
                        new SelectListItem
                        {
                            Value = FiscalQuarters.Q2.ToString(),
                            Text = $"{FiscalQuarters.Q2} {DateTime.Now.Year}"
                        },
                        new SelectListItem
                        {
                            Value = FiscalQuarters.Q3.ToString(),
                            Text = $"{FiscalQuarters.Q3} {DateTime.Now.Year}"
                        },
                        new SelectListItem
                        {
                            Value = FiscalQuarters.Q4.ToString(),
                            Text = $"{FiscalQuarters.Q4} {DateTime.Now.Year}"
                        },
                    }
                }
            );
        }

        // GET: Salespersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salespersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Address,PhoneNumber,StartDate,TerminationDate,Manager")] Salesperson salesperson)
        {
            if (ModelState.IsValid)
            {
                if (_context.Salespeople.Any(s => s.FirstName == salesperson.FirstName.Trim() && s.LastName == salesperson.LastName.Trim()))
                {
                    ViewBag.ErrorMessage = "This salesperson already exists in the system";
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
            if (id == null)
            {
                return NotFound();
            }

            var salesperson = await _context.Salespeople.FindAsync(id);
            if (salesperson == null)
            {
                return NotFound();
            }
            return View(salesperson);
        }

        // POST: Salespersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Address,PhoneNumber,StartDate,TerminationDate,Manager")] Salesperson salesperson)
        {
            if (id != salesperson.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalespersonExists(salesperson.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salesperson);
        }

        // GET: Salespersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesperson = await _context.Salespeople
                .FirstOrDefaultAsync(m => m.id == id);
            if (salesperson == null)
            {
                return NotFound();
            }

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

        private bool SalespersonExists(int id)
        {
            return _context.Salespeople.Any(e => e.id == id);
        }
    }
}
