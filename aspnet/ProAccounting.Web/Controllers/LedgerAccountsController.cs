using Microsoft.AspNetCore.Mvc;
using ProAccounting.Application;
using ProAccounting.Application.Services.LedgerAccounts.Dto;
using ProAccounting.Application.Interfaces;

namespace ProAccounting.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerAccountsController(ILedgerAccountService ledgerAccountService) : ControllerBase
    {
        private readonly ILedgerAccountService _ledgerAccountService = ledgerAccountService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLedgerAccountInput input)
        {
            try
            {
                await _ledgerAccountService.Create(input);
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
                await _ledgerAccountService.Delete(id);
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
            var clients = await _ledgerAccountService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await _ledgerAccountService.GetById(id);
                return Ok(client);
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
        public async Task<IActionResult> Update([FromBody] UpdateLedgerAccountInput input)
        {
            try
            {
                await _ledgerAccountService.Update(input);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
