using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WasmahTask.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice invoice { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }
    }
}
