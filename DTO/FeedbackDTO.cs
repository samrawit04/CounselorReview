namespace counselorReview.DTO
{
    public class FeedbackDTO
    {
        public string? Id { get; set; }
        public string? ClientId { get; set; }
        public string? CounselorId { get; set; } 
        public string? ClientName { get; set; } 
        public string? CounselorName { get; set; } 
        public string? Comment { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
