using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class InvoiceDTORepository : IRepository<InvoiceDTO>
    {
        private readonly IRepository<Invoice> invoiceRepository;

        public InvoiceDTORepository(IRepository<Invoice>invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }
        public InvoiceDTO Add(InvoiceDTO Item)
        {
            Invoice invoice = new Invoice();
            invoice.Total = Item.Total;
            invoice.Items = new List<InvoiceItem>();
            foreach(var item in Item.Items)
            {
                invoice.Items.Add(new InvoiceItem()
                {
                    ProductId = item.Id,
                    Quantity = item.Quantity
                });

            }
            invoiceRepository.Add(invoice);
            return Item;
        }

        public InvoiceDTO Delete(int Id)
        {
            Invoice invoice = invoiceRepository.Delete(Id);
            if (invoice == null)
                return null;
            InvoiceDTO invoiceDTO = new InvoiceDTO()
            {
                Id = invoice.Id,
                Total = invoice.Total
            };
            invoiceDTO.Items = new List<InvoiceItemDTO>();
            foreach(var item in invoice.Items)
            {
                invoiceDTO.Items.Add(new InvoiceItemDTO()
                {
                    Id=item.ProductId,
                    Description=item.product.Description,
                    Name=item.product.Name,
                    Quantity=item.Quantity,
                    UnitPrice=item.product.UnitPrice,
                    Total=item.Quantity*item.product.UnitPrice
                });
            }
            return invoiceDTO;
        }

        public InvoiceDTO Get(int Id)
        {
            Invoice invoice = invoiceRepository.Get(Id);
            if (invoice == null)
                return null;
            InvoiceDTO invoiceDTO = new InvoiceDTO()
            {
                Id = invoice.Id,
                Total = invoice.Total
            };
            invoiceDTO.Items = new List<InvoiceItemDTO>();
            foreach (var item in invoice.Items)
            {
                invoiceDTO.Items.Add(new InvoiceItemDTO()
                {
                    Id = item.ProductId,
                    Description = item.product.Description,
                    Name = item.product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.product.UnitPrice,
                    Total = item.Quantity * item.product.UnitPrice
                });
            }
            return invoiceDTO;
        }

        public InvoiceDTO Update(InvoiceDTO Item)
        {
            Invoice invoice = invoiceRepository.Get(Item.Id);
            if (invoice == null)
                return null;
            invoice.Total = Item.Total;
            invoice.Items.Clear();
            foreach (var item in Item.Items)
            {
                invoice.Items.Add(new InvoiceItem()
                {
                    ProductId = item.Id,
                    Quantity = item.Quantity
                });
            }
            invoiceRepository.Update(invoice);
            return Item;
        }
    }
}
