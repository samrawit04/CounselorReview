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
        public async Task<IActionResult> Signup([FromBody] CreateClientDTO clientDto)
        {
            try
            {
                var newClient = new Client
                {
                    FullName = clientDto.FullName,
                    Email = clientDto.Email,
                    Password = clientDto.Password
                };

                var registeredClient = await _clientService.(newClient);

                if (registeredClient == null)
                {
                    return BadRequest(new { message = "Failed to register the client." });
                }

                var clientResponse = new UserResponseDTO
                {
                    Id = registeredClient.Id ?? string.Empty, 
                    FullName = registeredClient.FullName ?? string.Empty, 
                    Email = registeredClient.Email ?? string.Empty, 
                    CreatedAt = registeredClient.CreatedAt,
                    UpdatedAt = registeredClient.UpdatedAt
                };

                return CreatedAtAction(nameof(Signup), new { id = registeredClient.Id }, clientResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
public async Task<IActionResult> GetAllClients() => Ok(await _clientService.GetAllClientsAsync());

[HttpGet("{id}")]
public async Task<IActionResult> GetClientById(string id)
{
    var client = await _clientService.GetClientByIdAsync(id);
    return client == null ? NotFound() : Ok(client);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateClient(string id, [FromBody] UpdateUserDTO dto)
{
    var existingClient = await _clientService.GetClientByIdAsync(id);
    if (existingClient == null)
    {
        return NotFound();
    }

    var updatedClient = new Client
    {
        Id = id,
        FullName = dto.FullName ?? existingClient.FullName,  
        Email = dto.Email ?? existingClient.Email, 
        Password = existingClient.Password,  
        UpdatedAt = DateTime.UtcNow  
    };

    var result = await _clientService.UpdateClientAsync(id, updatedClient);

    return result == null ? NotFound() : Ok(result);
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteClient(string id) => await _clientService.DeleteClientAsync(id) ? Ok() : NotFound();
      
    }
}
