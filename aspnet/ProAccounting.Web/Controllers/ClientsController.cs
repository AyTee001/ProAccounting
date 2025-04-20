using Microsoft.AspNetCore.Mvc;
using ProAccounting.Application;
using ProAccounting.Application.Interfaces;
using ProAccounting.Application.Services.Clients.Dto;

namespace ProAccounting.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientInput input)
        {
            try
            {
                await _clientService.Create(input);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest();
            }
            catch (Exception) {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clientService.Delete(id);
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
            var clients = await _clientService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var client = await _clientService.GetById(id);
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
        public async Task<IActionResult> Update([FromBody] UpdateClientInput input)
        {
            try
            {
                await _clientService.Update(input);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
