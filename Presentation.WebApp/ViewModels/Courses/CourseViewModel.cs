using Presentation.WebApp.Models.Course;


namespace Presentation.WebApp.ViewModels.Courses;

public class CourseViewModel
{
    public string Id { get; set; } = null!;
    public string CourseTitle { get; set; } = null!;
    public string CourseIngress { get; set; } = null!;
    public string CourseDescription { get; set; } = null!;
    public string? CourseImageUrl { get; set; }
    public bool IsBestseller { get; set; } = false;
    public string CourseCategory { get; set; } = null!;
    public RatingModel Rating { get; set; } = new();
    public PriceModel Price { get; set; } = new();
    public IncludedModel Included { get; set; } = new();
    public AuthorModel Author { get; set; } = new();
    public List<ProgramDetailsModel> Content { get; set; } = [];

}