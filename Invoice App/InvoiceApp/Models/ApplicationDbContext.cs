using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Prog;Initial Catalog=WasmahTask;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii =>ii.invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceId);
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.product)
                .WithMany(i => i.Invoices)
                .HasForeignKey(ii => ii.ProductId);
            Product product1 = new Product
            {
                Id=-1,
                Name = "IPhone X",
                Description = "Made by Apple company",
                UnitPrice = 23000,
                AvailableQuantity = 10,
            };
            Product product2 = new Product
            {
                Id = -2,
                Name = "Samsung Note 8",
                Description = "Made by Samsung company",
                UnitPrice = 12000,
                AvailableQuantity = 8,
            };
            Product product3 = new Product
            {
                Id = -3,
                Name = "Oppo A5",
                Description = "Made by Oppo company",
                UnitPrice = 4000,
                AvailableQuantity = 25,
            };
            Product product4 = new Product
            {
                Id = -4,
                Name = "Hawaui E9",
                Description = "Made by Hawaui company",
                UnitPrice = 6500,
                AvailableQuantity = 12,
            };
            modelBuilder.Entity<Product>().HasData(new[] { product1, product2, product3, product4 });
        }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
    }
}
