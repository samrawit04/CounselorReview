namespace counselorReview.DTO
{
    public class CreateUserDTO
    {
        
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    public List<string> Specializations { get; set; } = new List<string>();

    }
}
