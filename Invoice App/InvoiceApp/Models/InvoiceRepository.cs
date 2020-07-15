using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private readonly ApplicationDbContext context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Invoice Add(Invoice Item)
        {
            foreach(var item in Item.Items)
            {
                var product = context.Products.FirstOrDefault(prd => prd.Id == item.ProductId);
                product.AvailableQuantity -= item.Quantity;
            }
            context.Invoices.Add(Item);
            context.SaveChanges();
            return Item;
        }

        public Invoice Delete(int Id)
        {
            Invoice invoice = context.Invoices.Include(inv => inv.Items).ThenInclude(p => p.product).FirstOrDefault(inv => inv.Id == Id);
            if (invoice != null)
            {
                foreach(var item in invoice.Items)
                {
                    var product = context.Products.FirstOrDefault(prd => prd.Id == item.ProductId);
                    product.AvailableQuantity += item.Quantity;
                }
                context.Remove(invoice);
                context.SaveChanges();
            }
            return invoice;
        }

        public Invoice Get(int Id)
        {
            return context.Invoices.Include(inv=>inv.Items).ThenInclude(p=>p.product).FirstOrDefault(inv=>inv.Id==Id);
        }

        public Invoice Update(Invoice Item)
        {
            Invoice invoice= context.Invoices.Include(inv => inv.Items).ThenInclude(p => p.product).FirstOrDefault(inv => inv.Id == Item.Id);
            foreach(var item in invoice.Items)
            {
                var product = context.Products.FirstOrDefault(prd => prd.Id == item.ProductId);
                product.AvailableQuantity += item.Quantity;
            }
            foreach (var item in Item.Items)
            {
                var product = context.Products.FirstOrDefault(prd => prd.Id == item.ProductId);
                product.AvailableQuantity -= item.Quantity;
            }
            context.SaveChanges();
            var invoiceEntity= context.Invoices.Attach(Item);
            invoiceEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Item;
        }
    }
}
