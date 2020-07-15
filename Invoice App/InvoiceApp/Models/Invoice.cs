using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public virtual ICollection<InvoiceItem> Items { get; set; }
    }
}
