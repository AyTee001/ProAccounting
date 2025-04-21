using Microsoft.AspNetCore.Mvc;
using ProAccounting.Application;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services.Invoices.Dto;

namespace ProAccounting.Web.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceInput input)
        {
            try
            {
                await _invoiceService.Create(input);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _invoiceService.Delete(id);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _invoiceService.GetAll();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetById(id);
                return Ok(invoice);
            }
            catch (BusinessException ex)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateInvoiceInput input)
        {
            try
            {
                await _invoiceService.Update(input);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
