using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Course;

public class CreateCourseDto
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Ingress { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Price { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    [Required]
    public string Hours { get; set; } = null!;
    public string? LikesInNumbers { get; set; }
    public string? LikesInProcent { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsBestSeller { get; set; } = false;
    public string? Articles { get; set; }
    public string? Resources { get; set; }
    public bool LifetimeAccess { get; set; } = false;
    public bool Certificate { get; set; } = false;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string AuthorBiography { get; set; } = null!;
    public string? AuthorImageUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? FollowersYoutube { get; set; }
    public string? FacebookUrl { get; set; }
    public string? FollowersFacebook { get; set; }
}