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
                    Email = counselorDto.Email,
                    Password = counselorDto.Password
                };

                var registeredCounselor = await _counselorService.RegisterCounselorAsync(newCounselor);

                return CreatedAtAction(nameof(Signup), 
                    new { id = registeredCounselor.Id }, 
                    new { registeredCounselor.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }                                                                                                                                                                                    
        }

    }
}
