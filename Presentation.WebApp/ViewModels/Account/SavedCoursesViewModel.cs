using Presentation.WebApp.Models.Courses;

namespace Presentation.WebApp.ViewModels.Account;

public class SavedCoursesViewModel
{
    public bool IsSuccces { get; set; } = false;
    public IEnumerable<CourseModel>? Courses { get; set; }
}
