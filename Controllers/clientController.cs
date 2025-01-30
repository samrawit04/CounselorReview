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
                // Validate and create a new client object from the DTO
                var newClient = new Client
                {
                    FullName = clientDto.FullName,
                    Email = clientDto.Email,
                    Password = clientDto.Password
                };

                var registeredClient = await _clientService.RegisterClientAsync(newClient);

                // Ensure the registeredClient is not null
                if (registeredClient == null)
                {
                    return BadRequest(new { message = "Failed to register the client." });
                }

                // Map the registered client to a response DTO and provide default values if necessary
                var clientResponse = new UserResponseDTO
                {
                    Id = registeredClient.Id ?? string.Empty, // Handle potential null for Id
                    FullName = registeredClient.FullName ?? string.Empty, // Handle potential null for FullName
                    Email = registeredClient.Email ?? string.Empty, // Handle potential null for Email
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

    // Create an updated client, but keep the existing password if not updating it.
    var updatedClient = new Client
    {
        Id = id,
        FullName = dto.FullName ?? existingClient.FullName,  // Update FullName if provided
        Email = dto.Email ?? existingClient.Email,  // Update Email if provided
        Password = existingClient.Password,  // Keep the old password
        UpdatedAt = DateTime.UtcNow  // Update the UpdatedAt timestamp
    };

    var result = await _clientService.UpdateClientAsync(id, updatedClient);

    return result == null ? NotFound() : Ok(result);
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteClient(string id) => await _clientService.DeleteClientAsync(id) ? Ok() : NotFound();
      
    }
}
