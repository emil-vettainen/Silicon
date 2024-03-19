using Infrastructure.Entities.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
    public async Task <IActionResult> Courses()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7107/api/mongo");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);
        return View(data);
    }




    public async Task<IActionResult> SingleCourse(string id)
    {
        using var http = new HttpClient();
        var response = await http.GetAsync($"https://localhost:7107/api/mongo/{id}");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<CourseEntity>(json);

        return View(data);
    }


}
