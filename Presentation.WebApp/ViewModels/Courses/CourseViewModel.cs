using Presentation.WebApp.Models.Courses;


namespace Presentation.WebApp.ViewModels.Courses;

public class CourseViewModel
{
    public bool IsSuccess { get; set; } = false;
    public IEnumerable<string>? Categories { get; set; }
    public IEnumerable<CourseModel>? Courses { get; set; }
    public PaginationModel? Pagination { get; set; }
}