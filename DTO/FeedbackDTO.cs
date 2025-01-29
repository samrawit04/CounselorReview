namespace counselorReview.DTO
{
    public class FeedbackDTO
    {
        public string? Id { get; set; }
        public string ClientId { get; set; } = string.Empty; // Client who gave the feedback
        public string CounselorId { get; set; } = string.Empty; // Counselor who receives the feedback
        public string Content { get; set; } = string.Empty; // Feedback content/message
        public DateTime CreatedAt { get; set; }
    }
}
