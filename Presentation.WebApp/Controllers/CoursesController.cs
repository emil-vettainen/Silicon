using Business.Dtos.Course;
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
    public async Task<IActionResult> UpdateCoursesByFilter(string? category, string? searchQuery)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?category={category}&searchQuery={searchQuery}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(json);
                return PartialView("~/Views/Courses/_CourseBoxesPartial.cshtml", data);
            }
        }
        catch (Exception)
        {
            //logger
        }
        return RedirectToAction("Courses");
    }


    [HttpPost]
    public async Task<IActionResult> Favorite(string id)
    {

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
    {
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> EditCourse(string id)
    {
        return Ok();
    }


   
}