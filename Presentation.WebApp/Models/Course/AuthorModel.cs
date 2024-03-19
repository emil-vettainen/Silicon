namespace Presentation.WebApp.Models.Courses;

public class AuthorModel
{
    public string FullName { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public string YouTubeUrl { get; set; } = null!;
    public string Subscribers { get; set; } = null!;
    public string FacebookUrl { get; set; } = null!;
    public string Followers { get; set; } = null!;
}