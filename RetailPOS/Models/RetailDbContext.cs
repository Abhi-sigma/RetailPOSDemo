using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System;

namespace RetailPOS.Models
{
    public class RetailDbContext : DbContext


    {
        public RetailDbContext(DbContextOptions<RetailDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OfflineSales> OfflineSales { get; set; }

        public DbSet<LineSaleItems> LineSaleItems { get; set; }

        public DbSet<StockMeasureType> StockMeasureType { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=RetailPOS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
