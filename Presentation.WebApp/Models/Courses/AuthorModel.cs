namespace Presentation.WebApp.Models.Course;

public class AuthorModel
{
    public string FullName { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public SocialMediaModel? SocialMedia { get; set; }
}
