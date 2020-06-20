using BeSpoked.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Data
{
    public class BeSpokedContext: DbContext
    {
        public BeSpokedContext(DbContextOptions<BeSpokedContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Salesperson> Salespeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Not sure if this is needed yet, may only be needed for linked tables
        }

    }
}
