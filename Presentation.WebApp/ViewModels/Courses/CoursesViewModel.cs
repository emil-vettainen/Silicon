namespace Presentation.WebApp.ViewModels.Courses;

public class CoursesViewModel
{
    public string CourseImageUrl { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public string CourseAuthor {  get; set; } = null!;
    public decimal CoursePrice { get; set; }
    public string CourseHour { get; set; } = null!;
    public string CourseReview { get; set; } = null!;
}
