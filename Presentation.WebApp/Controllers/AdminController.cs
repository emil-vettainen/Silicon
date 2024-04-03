using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Courses;
using System.Net.Http;

namespace Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AdminController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Dashboard()
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
    }
}