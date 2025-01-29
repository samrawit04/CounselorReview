using counselorReview.Models;

namespace counselorReview.Services
{
    public interface ICounselorService
    {
        Task<IEnumerable<Counselor>> GetAllCounselorsAsync();
        Task<Counselor> GetCounselorByIdAsync(string id);
        Task CreateCounselorAsync(Counselor counselor);
        Task UpdateCounselorAsync(string id, Counselor counselor);
        Task DeleteCounselorAsync(string id);
    }
}
