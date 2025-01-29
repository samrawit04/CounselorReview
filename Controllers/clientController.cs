using counselorReview.DTO;
using counselorReview.Models;
using counselorReview.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace counselorReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
         [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] CreateClientDTO createClientDto)
    {
        try
        {
            var client = await _clientService.SignUpAsync(createClientDto);
            return CreatedAtAction(nameof(SignUp), new { id = client.Id }, client);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

      
    }
}
