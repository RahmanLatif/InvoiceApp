using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public string Description { get; set; }
        public virtual ICollection<InvoiceItem> Invoices { get; set; }
    }
}
