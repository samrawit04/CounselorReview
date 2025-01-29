using System.Collections.Generic;
using System.Threading.Tasks;
using counselorReview.Models;

namespace counselorReview.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
        Task<Feedback> GetFeedbackByIdAsync(string id);
        Task CreateFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(string id, Feedback feedback);
        Task DeleteFeedbackAsync(string id);
    }
}
