using Infrastructure.Entities.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Courses;

namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
    [HttpGet]
    public async Task <IActionResult> Courses()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7011/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(json);
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> SingleCourse(string id)
    {
        using var http = new HttpClient();
        var response = await http.GetAsync($"https://localhost:7107/api/mongo/{id}");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<CourseEntity>(json);

        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> Favorite(string Id)
    {
        return View();
    }
}