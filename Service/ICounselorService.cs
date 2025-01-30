using counselorReview.DTO;
using counselorReview.Models;

namespace counselorReview.Services
{
    public interface ICounselorService
    {
        Task<Counselor> RegisterCounselorAsync(Counselor counselor);
        Task<List<Counselor>> GetAllCounselorsAsync();
        Task<CounselorDTO> GetCounselorByIdAsync(string id);
        Task<CounselorDTO> UpdateCounselorAsync(string id, CounselorDTO updateDto);
        Task<bool> DeleteCounselorAsync(string id);
    }
}
