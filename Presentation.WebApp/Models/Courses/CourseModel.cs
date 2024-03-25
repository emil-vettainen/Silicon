namespace Presentation.WebApp.Models.Course;

public class CourseModel
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string? DiscountPrice { get; set; }
    public string? CourseImgUrl { get; set; }
}