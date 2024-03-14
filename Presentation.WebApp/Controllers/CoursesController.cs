using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
    public IActionResult Courses()
    {
        return View();
    }
}
