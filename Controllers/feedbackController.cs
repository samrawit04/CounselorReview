using Microsoft.AspNetCore.Mvc;
using counselorReview.Models;
using counselorReview.Services;
using counselorReview.DTO;

namespace counselorReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackDTO dto)
        {
            var createdFeedback = await _feedbackService.CreateFeedbackAsync(dto);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = createdFeedback.Id }, createdFeedback);
        }

       
        [HttpGet]
        public async Task<ActionResult<List<FeedbackDTO>>> GetAllFeedback()
        {
            var feedbacks = await _feedbackService.GetAllFeedbackAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDTO>> GetFeedbackById(string id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
                return NotFound(new { message = "Feedback not found" });

            return Ok(feedback);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<FeedbackDTO>>> SearchFeedbackByClientName([FromQuery] string clientName)
        {
            var feedbacks = await _feedbackService.SearchFeedbackByClientNameAsync(clientName);
            return Ok(feedbacks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(string id, [FromBody] CreateFeedbackDTO dto)
        {
            var updated = await _feedbackService.UpdateFeedbackAsync(id, dto);
            if (!updated)
                return NotFound(new { message = "Feedback not found" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(string id)
        {
            var deleted = await _feedbackService.DeleteFeedbackAsync(id);
            if (!deleted)
                return NotFound(new { message = "Feedback not found" });

            return NoContent();
        }
      
    }
}
