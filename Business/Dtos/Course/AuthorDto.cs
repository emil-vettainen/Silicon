namespace Business.Dtos.Course;

public class AuthorDto
{
    public string FullName { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public SocialMediaDto? SocialMedia { get; set; }
}
