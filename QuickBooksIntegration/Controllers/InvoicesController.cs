using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuickBooksIntegration.Controllers
{
    using Application.Dtos;
    using Application.Services.Abstractions;
    using Infrastructure.Integrations.QuickBooks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly QuickBooksAuthService _quickBooksService;
        private readonly IInvoicesService invoicesService;

        public InvoicesController(QuickBooksAuthService quickBooksService, IInvoicesService invoicesService)
        {
            _quickBooksService = quickBooksService;
            this.invoicesService = invoicesService;
        }

        // GET: api/invoices
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            try
            {
                var invoices = await invoicesService.GetInvoicesAsync("QuickBookToken");
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/invoices
        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoice)
        {
            if (invoice == null)
            {
                return BadRequest("Invoice data is null.");
            }

            try
            {
                var createdInvoice = await invoicesService.CreateInvoiceAsync("QuickBookToken", invoice);
                return CreatedAtAction(nameof(GetInvoices), new { id = createdInvoice.Id }, createdInvoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
