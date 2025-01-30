namespace counselorReview.DTO
{
    public class CreateFeedbackDTO
    {
        public string ClientId { get; set; } = string.Empty; // Client who gives the feedback
        public string CounselorId { get; set; } = string.Empty; // Counselor who receives the feedback
        public string Comment { get; set; } = string.Empty; // The feedback message/content
    }
}
