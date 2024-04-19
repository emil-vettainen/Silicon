using AutoMapper;
using Business.Dtos.Course;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.Models.Courses;
using Presentation.WebApp.ViewModels.Admin;
using Presentation.WebApp.ViewModels.Courses;
using Shared.Responses.Enums;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Presentation.WebApp.Controllers;



[Authorize(Roles = "Admin")]
public class AdminController(HttpClient httpClient, IConfiguration configuration, CourseService courseService, IMapper mapper) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;
    private readonly CourseService _courseService = courseService;
    private readonly IMapper _mapper = mapper;


    #region Dashboard
    [HttpGet]
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
                    IsSuccess = true,
                    Courses = result?.Courses ?? []
                };
                return View(viewModel);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return View(new CourseViewModel());
    }
    #endregion


    #region Create Course
    [HttpGet]
    public IActionResult CreateCourse()
    {
        ViewData["Action"] = "CreateCourse";
        var viewModel = new CreateCourseViewModel();
        return View(viewModel);
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
                case ResultStatus.OK:
                    TempData["Success"] = "Course has been created";
                    return RedirectToAction("Dashboard", "Admin");

                case ResultStatus.EXISTS:
                    TempData["Warning"] = "Course with given title is already exists!";
                    return View(viewModel);

                default:
                    TempData["Error"] = "Something went wrong, please try again!";
                    return View(viewModel);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            TempData["Error"] = "The server encountered an unexpected condition!";
            return View(viewModel);
        }
    }
    #endregion


    #region Update Course
    [HttpGet]
    public async Task<IActionResult> UpdateCourse(string id)
    {
        var viewModel = new UpdateCourseViewModel();
        try
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}/{id}?key={_configuration["Api:Key"]}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());
                viewModel.Course = result ?? new CourseModel();
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCourse(UpdateCourseViewModel viewModel, IFormFile? courseImage, IFormFile? authorImage)
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
            var result = await _courseService.UpdateCourseApiAsync(_mapper.Map<UpdateCourseDto>(viewModel.Course), courseImage, authorImage, accessToken!);
            switch (result.StatusCode)
            {
                case ResultStatus.OK:
                    TempData["Success"] = "Course has been updated!";
                    return RedirectToAction("Dashboard", "Admin");

                case ResultStatus.EXISTS:
                    TempData["Warning"] = "Course with given title is already exists!";
                    return View(viewModel);

                default:
                    TempData["Error"] = "Something went wrong, please try again!";
                    return View(viewModel);

            }
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            TempData["Error"] = "The server encountered an unexpected condition!";
            return View(viewModel);
        }
    }
    #endregion


    #region Delete Course
    [HttpDelete]
    public async Task<IActionResult> DeleteCourse(string courseId)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Request.Cookies["AccessToken"]);
            var response = await _httpClient.DeleteAsync($"{_configuration["ApiUris:Courses"]}/{courseId}?key={_configuration["Api:Key"]}");
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500);
        }
        return NotFound();
    }
    #endregion

}