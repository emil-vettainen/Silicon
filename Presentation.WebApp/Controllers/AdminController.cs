using AutoMapper;
using Azure.Core;
using Business.Dtos.Course;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Admin;
using Presentation.WebApp.ViewModels.Courses;
using Shared.Factories;
using Shared.Responses.Enums;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Presentation.WebApp.Controllers
{
    

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly CourseService _courseService;
        private readonly IMapper _mapper;


        public AdminController(HttpClient httpClient, IConfiguration configuration, CourseService courseService, IMapper mapper)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?key={_configuration["Api:Key"]}");
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<CourseViewModel>(await response.Content.ReadAsStringAsync());
                    var viewModel = new CourseViewModel
                    {
                        Courses = result?.Courses ?? []
                    };
                    return View(viewModel);
                }

            }
            catch (Exception)
            {


            }

            return View();

            
        }


        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            try
            {
                var viewModel = new CreateCourseViewModel();
                
                return View(viewModel);

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseViewModel viewModel, IFormFile? courseImage, IFormFile? authorImage)
        {
            try
            {
                ModelState.Remove("courseImage");
                ModelState.Remove("authorImage");
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var accessToken = HttpContext.Request.Cookies["AccessToken"];
                var result = await _courseService.CreateCourseApiAsync(_mapper.Map<CreateCourseDto>(viewModel.Course), courseImage, authorImage, accessToken!);
                switch (result.StatusCode)
                {
                    case ResultStatus.CREATED:
                        TempData["Success"] = result.Message;
                        return RedirectToAction("Dashboard", "Admin");

                    default:
                        TempData["Error"] = result.Message;
                        return View(viewModel);

                }

            }
            catch (Exception)
            {
                TempData["Error"] = "Try again";
                return View(viewModel);
            }
           
        }
    }
}