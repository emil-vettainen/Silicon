using Presentation.WebApp.Models.Course;
using Presentation.WebApp.Models.Courses;

namespace Presentation.WebApp.ViewModels.Courses;

public class CourseViewModel
{
    public IEnumerable<AuthorModel> Authores { get; set; } = [];
    public CourseModel Course { get; set; } = new();
    public IncludedModel Included { get; set; } = new();
    public List<CourseContentModel> CoursesContent { get; set; } = [];
    

}