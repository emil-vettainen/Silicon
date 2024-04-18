using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.Models.Courses;
using Presentation.WebApp.ViewModels.Courses;
using System.Diagnostics;


namespace Presentation.WebApp.Controllers;

[Authorize]
public class CoursesController(HttpClient httpClient, IConfiguration configuration, CourseService courseService) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly CourseService _courseService = courseService;


    #region All Courses
    [Route("/courses")]
    [HttpGet]
    public async Task <IActionResult> Courses(string? category, string? searchQuery, int pageNumber = 1, int pageSize = 6)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?key={_configuration["Api:Key"]}&category={category}&searchQuery={searchQuery}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<CourseResultModel>(await response.Content.ReadAsStringAsync());

                var viewModel = new CourseViewModel
                {
                    IsSuccess = true,
                    Courses = result?.Courses ?? [],

                    Pagination = new PaginationModel
                    {
                        PageSize = pageSize,
                        CurrentPage = pageNumber,
                        TotalPages = result?.TotalPages ?? 0,
                        TotalItems = result?.TotalItems ?? 0
                    },

                    Categories = await _courseService.GetCategoriesAsync()
                };
                return View(viewModel);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                return View(new CourseViewModel
                {
                    IsSuccess = true,
                    NotFound = true,
                });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return View(new CourseViewModel());
    }
    #endregion


    #region Course Details
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            var viewModel = new CourseDetailsViewModel();
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}/{id}?key={_configuration["Api:Key"]}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CourseModel>(json);

                viewModel.Course = result ?? new CourseModel();

                return View(viewModel);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return View(new CourseDetailsViewModel());
    }
    #endregion


}