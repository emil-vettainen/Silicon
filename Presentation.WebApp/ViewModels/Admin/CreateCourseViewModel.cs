using Presentation.WebApp.Models.Course;
using Presentation.WebApp.Models.Courses;

namespace Presentation.WebApp.ViewModels.Admin;

public class CreateCourseViewModel
{
    public CreateCourseModel Course { get; set; } = new();

    public CreateCourseViewModel()
    {
        Course.Highlights =
        [
           new HighlightsModel()
        ];

        Course.Content =
        [
            new ProgramDetailsModel()
        ];
    }
}