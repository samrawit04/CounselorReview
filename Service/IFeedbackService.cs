using System.Collections.Generic;
using System.Threading.Tasks;
using counselorReview.DTO;
using counselorReview.Models;

namespace counselorReview.Services
{
    public interface IFeedbackService
    {
       Task<FeedbackDTO> CreateFeedbackAsync(CreateFeedbackDTO dto);
        Task<List<FeedbackDTO>> GetAllFeedbackAsync();
        Task<FeedbackDTO?> GetFeedbackByIdAsync(string id);
        Task<bool> UpdateFeedbackAsync(string id, CreateFeedbackDTO dto);
        Task<bool> DeleteFeedbackAsync(string id);
    }
}
