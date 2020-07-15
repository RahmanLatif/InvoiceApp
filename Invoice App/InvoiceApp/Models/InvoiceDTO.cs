using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public List<InvoiceItemDTO> Items { get; set; }
    }
}
