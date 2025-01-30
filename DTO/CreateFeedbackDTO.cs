namespace counselorReview.DTO
{
    public class CreateFeedbackDTO
    {
        public string ClientId { get; set; } = string.Empty; 
        public string CounselorId { get; set; } = string.Empty; 
        public string Comment { get; set; } = string.Empty; 
    }
}
