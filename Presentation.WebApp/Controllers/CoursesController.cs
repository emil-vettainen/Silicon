using Infrastructure.Entities.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Courses;


namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CoursesController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    [Route("/courses")]
    [HttpGet]
    public async Task <IActionResult> Courses()
    {
        //var viewModel = new CourseViewModel();
        //var response = await _httpClient.GetAsync("https://localhost:7011/api/courses");
        //viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

        try
        {
            var response = await _httpClient.GetAsync(_configuration["ApiUris:Courses"]);
            if (response.IsSuccessStatusCode)
            {
                var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(await response.Content.ReadAsStringAsync());
                return View(courses);

            }
         
        }
        catch (Exception)
        {

            
        }

        return View();

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

 
    [HttpGet]
    public async Task<IActionResult> FilterByCategory(string category)
    {
        try
        {
            var url = category == "All" ? "https://localhost:7011/api/courses" : $"https://localhost:7011/api/courses?category={category}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(json);
                return PartialView("~/Views/Courses/_CourseBoxesPartial.cshtml", data);
            }

           
        }
        catch (Exception)
        {

            throw;
        }
        return RedirectToAction("Courses");
    }


    [HttpPost]
    public async Task<IActionResult> Favorite(string id)
    {

        return Ok();
    }
}