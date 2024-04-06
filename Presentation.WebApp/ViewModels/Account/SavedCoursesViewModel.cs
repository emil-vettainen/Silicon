using Presentation.WebApp.Models.Courses;

namespace Presentation.WebApp.ViewModels.Account;

public class SavedCoursesViewModel
{
    public IEnumerable<CourseModel>? Courses { get; set; }
}
