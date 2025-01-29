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
        public async Task<IActionResult> Signup([FromBody] CreateUserDTO clientDto)
        {
            try
            {
                var newClient = new Client
                {
                    Email = clientDto.Email,
                    Password = clientDto.Password
                };

                var registeredClient = await _clientService.RegisterClientAsync(newClient);
                return CreatedAtAction(nameof(Signup), new { id = registeredClient.Id }, new { registeredClient.Email, registeredClient.CreatedAt });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }                                                                                                                                                                                    
        }
      
    }
}
