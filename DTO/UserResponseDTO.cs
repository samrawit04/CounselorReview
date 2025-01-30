public class UserResponseDTO
{
    public string? Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; }=string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}