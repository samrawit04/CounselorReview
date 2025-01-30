using Microsoft.AspNetCore.Mvc;
using counselorReview.Services;
using counselorReview.Models;
using Microsoft.Extensions.Logging;
using counselorReview.DTO;

namespace counselorReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounselorController : ControllerBase
    {
        private readonly ICounselorService _counselorService;

        public CounselorController(ICounselorService counselorService)
        {
            _counselorService = counselorService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CreateUserDTO counselorDto)
        {
            try
            {
                var newCounselor = new Counselor
                {
                    FullName = counselorDto.FullName,
                    Email = counselorDto.Email,
                    Password = counselorDto.Password,
                    Specializations = counselorDto.Specializations,
                };

                var registeredCounselor = await _counselorService.RegisterCounselorAsync(newCounselor);

                if (registeredCounselor == null)
                {
                    return BadRequest(new { message = "Failed to register the counselor." });
                }

                var counselorResponse = new UserResponseDTO
                {
                    Id = registeredCounselor.Id ?? string.Empty,
                    FullName = registeredCounselor.FullName ?? string.Empty,
                    Email = registeredCounselor.Email ?? string.Empty,
                    CreatedAt = registeredCounselor.CreatedAt,
                    UpdatedAt = registeredCounselor.UpdatedAt
                };

                return CreatedAtAction(nameof(Signup), new { id = registeredCounselor.Id }, counselorResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
       [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var counselors = await _counselorService.GetAllCounselorsAsync();
            return Ok(counselors);
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<CounselorDTO>> GetById(string id)
        {
            var counselor = await _counselorService.GetCounselorByIdAsync(id);
            if (counselor == null)
            {
                return NotFound(new { message = "Counselor not found." });
            }
            return Ok(counselor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CounselorDTO updateDto)
        {
            var updatedCounselor = await _counselorService.UpdateCounselorAsync(id, updateDto);
            if (updatedCounselor == null)
            {
                return NotFound(new { message = "Counselor not found." });
            }
            return Ok(updatedCounselor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _counselorService.DeleteCounselorAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Counselor not found." });
            }
            return NoContent();
        }


    }
}
