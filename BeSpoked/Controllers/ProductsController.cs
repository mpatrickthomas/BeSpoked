using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeSpoked.Data;
using BeSpoked.Data.Entities;
using System.Collections.Generic;

namespace BeSpoked.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BeSpokedContext _context;

        public ProductsController(BeSpokedContext context) => _context = context;

        // GET: Products
        public async Task<IActionResult> Index() => View(await _context.Products.OrderBy(p => p.Manufacturer).ToListAsync());

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create() => View();

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Manufacturer,Style,PurchasePrice,SalePrice,CurrentQuantity,ComissionPercentage")] Product product)
        {
            if (this.ModelState.IsValid)
            {
                if (_context.Products.Any(p => p.Name == product.Name.Trim() && p.Manufacturer == product.Manufacturer.Trim()))
                {
                    this.ViewBag.ErrorMessage = "This product already exists in the system";
                    return View(product);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Manufacturer,Style,PurchasePrice,SalePrice,CurrentQuantity,ComissionPercentage")] Product product)
        {
            if (id != product.id) return NotFound();

            if (this.ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var sales = _context.Sales.Where(s => s.Product.id == id).ToList();
            foreach (var sale in sales) _context.Sales.Remove(sale);

            var discounts = _context.Discounts.Where(d => d.Product.id == id).ToList();
            foreach (var discount in discounts) _context.Discounts.Remove(discount);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) => _context.Products.Any(e => e.id == id);
    }
}
