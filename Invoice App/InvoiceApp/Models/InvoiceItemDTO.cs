using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class InvoiceItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public float Total { get; set; }
    }
}
