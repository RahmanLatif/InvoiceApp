using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WasmahTask.Models;

namespace WasmahTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRepository<InvoiceDTO> repository;
        private readonly ApplicationDbContext context;

        public HomeController(IRepository<InvoiceDTO> repository,ApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = repository.Get(id);

            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = context.Products.Select(p => new { p.Id, p.Name, p.Description, p.AvailableQuantity, p.UnitPrice }).ToList();

            return Ok(products);
        }

        [HttpPut("{id}")]
        public IActionResult PutInvoice(int id, InvoiceDTO invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            try
            {
                repository.Update(invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult PostInvoice(InvoiceDTO invoice)
        {
            try
            {
                repository.Add(invoice);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            var invoice = repository.Get(id);
            if (invoice == null)
            {
                return NotFound();
            }

            try
            {
                repository.Delete(id);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(invoice);
        }
    }
}
