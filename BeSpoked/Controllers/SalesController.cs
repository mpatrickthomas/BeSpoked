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

namespace BeSpoked.Controllers
{
    public class SalesController : Controller
    {
        private readonly BeSpokedContext _context;

        public SalesController(BeSpokedContext context)
        {
            _context = context;
        }

        // GET: Sales
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.
        //        Sales
        //        .Include(s => s.Customer)
        //        .Include(s => s.Product)
        //        .Include(s => s.Salesperson)
        //        .ToListAsync());
        //}

        public async Task<IActionResult> Index(SaleIndexViewModel viewModel)
        {
            SaleIndexViewModel saleIndex = new SaleIndexViewModel
            {
                Sales = _context.
                Sales.Where(s => s.SaleDate >= (viewModel.StartDate ?? DateTime.MinValue) && s.SaleDate <= (viewModel.EndDate ?? DateTime.MaxValue))
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Include(s => s.Salesperson)
                .ToList(),
                StartDate = viewModel.StartDate?? DateTime.MinValue,
                EndDate = viewModel.EndDate ?? DateTime.MaxValue
            };
            return View(saleIndex);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .FirstOrDefaultAsync(m => m.id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public async Task<IActionResult> Create()
        {
            SaleCreateViewModel viewModel = new SaleCreateViewModel() {
                Salespeople = _context.Salespeople.Select(sp => new SelectListItem
                {
                    Value = sp.id.ToString(),
                    Text = $"{sp.FirstName} {sp.LastName}"
                }),
                Customers = _context.Customers.Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                }),
                Products = _context.Products.Select(p => new SelectListItem
                {
                    Value = p.id.ToString(),
                    Text = $"{p.Manufacturer} {p.Name}"
                }),
                Sale = new Sale()
            };

            return View(viewModel);
            //return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Sale sale = new Sale
                {
                    Salesperson = _context.Salespeople.First(sp => sp.id.ToString() == viewModel.SelectedSalesperson),
                    Customer = _context.Customers.First(c => c.id.ToString() == viewModel.SelectedCustomer),
                    Product = _context.Products.First(p => p.id.ToString() == viewModel.SelectedProduct),
                    SaleDate = viewModel.Sale.SaleDate
                };
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,SaleDate")] Sale sale)
        {
            if (id != sale.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.id))
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
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .FirstOrDefaultAsync(m => m.id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.id == id);
        }
    }
}
