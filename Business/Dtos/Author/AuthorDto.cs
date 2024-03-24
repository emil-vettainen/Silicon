namespace Business.Dtos.Author;

public class AuthorDto
{
    public string FullName { get; set; } = null!;
    public string AuthorBiography { get; set; } = null!;
    public string? AuthorImageUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? FollowersYoutube { get; set; }
    public string? FacebookUrl { get; set; }
    public string? FollowersFacebook { get; set; }
}
