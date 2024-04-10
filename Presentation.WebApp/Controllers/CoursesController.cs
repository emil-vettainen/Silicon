using AutoMapper;
using Business.Dtos.Course;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.Models.Courses;
using Presentation.WebApp.ViewModels.Courses;



namespace Presentation.WebApp.Controllers;

public class CoursesController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly CourseService _courseService;
    private readonly IMapper _mapper;

    public CoursesController(HttpClient httpClient, IConfiguration configuration, CourseService courseService, IMapper mapper)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _courseService = courseService;
        _mapper = mapper;
    }

    [Route("/courses")]
    [HttpGet]
    public async Task <IActionResult> Courses(string? category, string? searchQuery, int pageNumber = 1, int pageSize = 6)
    {
        try
        {
            //var courseResult = await _courseService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);
            //var viewModel = new CourseViewModel
            //{
            //    Courses = _mapper.Map<IEnumerable<CourseModel>>(courseResult),
            //    Pagination = new PaginationModel
            //    {
            //        PageSize = pageSize,
            //        CurrentPage = pageNumber,
            //        TotalPages = courseResult.TotalPages,
            //        TotalItems = courseResult.TotalItems
            //    }
            //};

            //return View(viewModel);

          


            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?key={_configuration["Api:Key"]}&category={category}&searchQuery={searchQuery}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseResultModel>(await response.Content.ReadAsStringAsync());


               

                var viewModel = new CourseViewModel
                {
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
        }
        catch (Exception)
        {
            
        }

        return View(new CourseViewModel());
       
    }

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
        catch (Exception)
        {
            //logger

            
        }
        return View(new CourseDetailsViewModel());

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
        return PartialView("~/Views/Courses/_CourseBoxesPartial.cshtml");
    }



    //[HttpPost]
    //public async Task<IActionResult> Favorite(string id)
    //{
    //    try
    //    {
    //        var result = await _courseService.SaveToFavoriteAsync(string id);

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
   


   
}