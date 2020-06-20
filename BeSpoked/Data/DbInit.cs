using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Data
{
    public class DbInit
    {
        public static void Initialize(BeSpokedContext context)
        {
            if (!context.Customers.Any())
            {
                Random startDateRandomizer = new Random();
                context.Customers.AddRange(new Entities.Customer[]
                {
                    new Entities.Customer
                    {
                        FirstName = "Lisa",
                        LastName = "Newcar",
                        StartDate = DateTime.UtcNow.AddYears(startDateRandomizer.Next(0, 5) * -1),
                        Address = "1234 Foobar Street",
                        PhoneNumber = "555-5555"
                    },
                    new Entities.Customer
                    {
                        FirstName = "Patty",
                        LastName = "Wagon",
                        StartDate = DateTime.UtcNow.AddDays(startDateRandomizer.Next(0, 5) * -1),
                        Address = "888 Theland Avenue",
                        PhoneNumber = "123-4567"
                    },
                    new Entities.Customer
                    {
                        FirstName = "Anita",
                        LastName = "Goodman",
                        StartDate = DateTime.UtcNow.AddDays(startDateRandomizer.Next(0, 5) * -1),
                        Address = "666 Shellfire Court",
                        PhoneNumber = "463-1029"
                    },
                    new Entities.Customer
                    {
                        FirstName = "Thelma",
                        LastName = "Annlouise",
                        StartDate = DateTime.UtcNow.AddDays(startDateRandomizer.Next(0, 5) * -1),
                        Address = "102 Gultch Street",
                        PhoneNumber = "309-1023"
                    }
                });

                context.SaveChanges();
            }

            if (!context.Salespeople.Any())
            {
                Random startDateRandomizer = new Random();
                context.Salespeople.AddRange(
                    new Entities.Salesperson[] {
                        new Entities.Salesperson
                        {
                            FirstName = "Gretchen",
                            LastName = "Weiners",
                            Address = "504 Seashell Boulevard",
                            StartDate = DateTime.UtcNow.AddYears(-2),
                            TerminationDate = null,
                            Manager = "Regina George",
                            PhoneNumber = "802-0158"
                        },
                        new Entities.Salesperson{
                            FirstName = "Nichelle",
                            LastName = "Nichols",
                            Address = "999 Enterprise Avenue",
                            StartDate = DateTime.UtcNow.AddYears(-5),
                            TerminationDate = null,
                            Manager = "Billy Shatner",
                            PhoneNumber = "987-1234"
                        },
                        new Entities.Salesperson
                        {
                            FirstName = "Breonna",
                            LastName = "Craquer",
                            Address = "203 Cheeseland Court",
                            StartDate = DateTime.UtcNow.AddDays(-256),
                            TerminationDate = DateTime.UtcNow.AddDays(-34),
                            Manager = "Robert Draquine",
                            PhoneNumber = "010-1005"
                        }
                    }
                );

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(new Entities.Product[]
                {
                    new Entities.Product
                    {
                        Name = "Try-cicle",
                        Manufacturer = "Simkea",
                        Style = "Red",
                        PurchasePrice = 55m,
                        SalePrice = 40.66m,
                        ComissionPercentage = 3m,
                        CurrentQuantity = 13,
                    },
                    new Entities.Product
                    {
                        Name = "Houghie Supreme",
                        Manufacturer = "Houghie",
                        Style = "Blue",
                        PurchasePrice = 102m,
                        SalePrice = 88m,
                        ComissionPercentage = 5m,
                        CurrentQuantity = 4
                    },
                    new Entities.Product
                    {
                        Name = "Houghie Mini",
                        Manufacturer = "Houghie",
                        Style = "Green",
                        PurchasePrice = 32m,
                        SalePrice = 29m,
                        ComissionPercentage = 15m,
                        CurrentQuantity = 11
                    },
                    new Entities.Product
                    {
                        Name = "Toofer",
                        Manufacturer = "Nottreal",
                        Style = "Tandem",
                        PurchasePrice = 55m*2,
                        SalePrice = (55m*2) - 13,
                        ComissionPercentage = 6m,
                        CurrentQuantity = 3
                    }
                });

                context.SaveChanges();
            }

            if (!context.Discounts.Any())
            {
                context.Discounts.AddRange(new Entities.Discount[]
                {
                    new Entities.Discount
                    {
                        BeginDate = DateTime.UtcNow.AddDays(-7),
                        DiscountPercentage = 10m,
                        EndDate = DateTime.UtcNow.AddDays(3),
                        Product = context.Products.ToList().FirstOrDefault(),
                    },
                    new Entities.Discount
                    {
                        BeginDate = DateTime.UtcNow,
                        DiscountPercentage = 10m,
                        EndDate = DateTime.UtcNow.AddDays(1),
                        Product = context.Products.ToList().LastOrDefault(),
                    }
                });

                context.SaveChanges();
            }

            if (!context.Sales.Any())
            {
                context.Sales.AddRange(new Entities.Sale[]
                {
                    new Entities.Sale
                    {
                        Customer = context.Customers.ToList().FirstOrDefault(),
                        SaleDate = DateTime.UtcNow.AddDays(-2),
                        Salesperson = context.Salespeople.ToList().FirstOrDefault(),
                        Product = context.Products.ToList().FirstOrDefault()
                    }
                });

                context.SaveChanges();
            }


            //context.SaveChanges(); // TODO async?
        }
    }
}
