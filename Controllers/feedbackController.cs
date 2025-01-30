using Microsoft.AspNetCore.Mvc;
using counselorReview.Models;
using counselorReview.Services;

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

      
    }
}
