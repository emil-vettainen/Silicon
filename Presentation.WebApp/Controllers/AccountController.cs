using AutoMapper;
using Business.Factories;
using Business.Services;
using Infrastructure.Entities.AccountEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using Newtonsoft.Json;
using Presentation.WebApp.Models;
using Presentation.WebApp.Models.Account;
using Presentation.WebApp.Models.Courses;
using Presentation.WebApp.ViewModels.Account;
using Presentation.WebApp.ViewModels.Courses;
using Shared.Responses.Enums;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserService _userService;
    private readonly AddressService _addressService;
    private readonly UserManager<UserEntity> _userManager;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;


    public AccountController(UserManager<UserEntity> userManager, UserService userService, AddressService addressService, HttpClient httpClient, IConfiguration configuration, IMapper mapper)
    {
        _userManager = userManager;
        _userService = userService;
        _addressService = addressService;
        _httpClient = httpClient;
        _configuration = configuration;
        _mapper = mapper;
    }

    #region Details
    public async Task<IActionResult> Details()
    {
        var userId = _userManager.GetUserId(User);
        var userInfo = await _userService.GetBasicInfoAsync(userId!);
        var addressInfo = await _addressService.GetAddressInfoAsync(userId!);

        var viewModel = new AccountDetailsViewModel
        {
            IsExternalAccount = userInfo.IsExternalAccount,
            BasicInfo = new BasicInfoModel { FirstName = userInfo.FirstName, LastName = userInfo.LastName, Email = userInfo.Email, Phone = userInfo.PhoneNumber, Biography = userInfo.Biography },
            AddressInfo = addressInfo != null ? new AddressInfoModel { Addressline_1 = addressInfo.StreetName, Addressline_2 = addressInfo.OptionalAddress, PostalCode = addressInfo.PostalCode, City = addressInfo.City } : null
        };
        return View(viewModel);
    }
    #endregion


    #region Details [HttpPost] 
    [HttpPost]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel, string action)
    {
        var userId = _userManager.GetUserId(User);
        var userInfo = await _userService.GetBasicInfoAsync(userId!);
        var addressInfo = await _addressService.GetAddressInfoAsync(userId!);

        switch (action)
        {
            case "basicinfo":
                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
                {
                    var response = await _userService.UpdateUserAsync(userId!, UserFactory.UpdateUserDto(viewModel.BasicInfo.FirstName, viewModel.BasicInfo.LastName, viewModel.BasicInfo.Email, viewModel.BasicInfo.Phone!, viewModel.BasicInfo.Biography!, userInfo.IsExternalAccount));
                    switch (response.StatusCode)
                    {
                        case ResultStatus.OK:
                            ViewBag.Success = "Your basic info has been updated!";
                            break;

                        default:
                            ViewBag.Error = "Something went wrong, please try again!";
                            break;
                    }
                }
                break;

            case "addressinfo":
                if(viewModel.AddressInfo!.Addressline_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {
                    var respone = await _addressService.CreateOrUpdateAddressInfoAsync(AddressFactory.CreateAddressDto(viewModel.AddressInfo.Addressline_1, viewModel.AddressInfo.Addressline_2, viewModel.AddressInfo.PostalCode, viewModel.AddressInfo.City), userId!);
                    switch (respone.StatusCode)
                    {
                        case ResultStatus.OK:
                            ViewBag.Success = "Your address info has been updated";
                                break;
                        default:
                            ViewBag.Error = "Something went wrong, please try again";
                            break;
                    }
                }
                break;
        }
        viewModel.BasicInfo ??= new BasicInfoModel { FirstName = userInfo.FirstName, LastName = userInfo.LastName, Email = userInfo.Email, Phone = userInfo.PhoneNumber, Biography = userInfo.Biography };
        viewModel.AddressInfo ??= new AddressInfoModel { Addressline_1 = addressInfo.StreetName, Addressline_2 = addressInfo.OptionalAddress, PostalCode = addressInfo.PostalCode, City = addressInfo.City };
        viewModel.IsExternalAccount = userInfo.IsExternalAccount;
        
        return View(viewModel);
    }
    #endregion


    #region Security
    [HttpGet]
    public async Task<IActionResult> Security()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId!);

        var viewModel = new SecurityViewModel
        {
            IsExternalAccount = user!.IsExternalAccount
        };

        return View(viewModel);
    }
    #endregion


    #region Security [HttpPost] 
    [HttpPost]
    public async Task<IActionResult> Security(SecurityViewModel viewModel, string action)
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId!);

        switch (action)
        {
            case "password":
                var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
                if (viewModel.ChangePassword.CurrentPassword != null && Regex.IsMatch(viewModel.ChangePassword.NewPassword, pattern) && viewModel.ChangePassword.NewPassword == viewModel.ChangePassword.ConfirmPassword)
                {
                    var result = await _userManager.ChangePasswordAsync(user!, viewModel.ChangePassword.CurrentPassword, viewModel.ChangePassword.NewPassword);
                    if (!result.Succeeded)
                    {
                        ViewBag.Error = "An unexpected error occurred.";
                        return View(viewModel);
                    }
                    ViewBag.Success = "Your password was changed successfully";
                }
                break;

            case "delete":
                if (viewModel.DeleteAccount.DeleteAccount)
                {
                    var result = await _userManager.DeleteAsync(user!);
                    if (!result.Succeeded)
                    {
                        ViewBag.Error = "An unexpected error occurred.";
                        return View(viewModel);
                    }
                    return RedirectToAction("SignIn", "Authentication");
                }
                break;
        }
        viewModel.IsExternalAccount = user!.IsExternalAccount;
        return View(viewModel); 
    }
    #endregion





    [HttpPost]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        var result = await _userService.UploadProfileImageAsync(User, file);
        if (result)
        {
            ViewBag.Success = "Profile image have been updated!";
        }
        return RedirectToAction("Details", "Account");

    }



    //#region ProfileImage [HttpPost]
    //[HttpPost]
    //public async Task <IActionResult> UpdateProfileImage(AccountPanelViewModel viewModel)
    //{
    //    var userId = _userManager.GetUserId(User);

    //    if (viewModel.ProfileImage != null)
    //    {

    //        var result = await _userService.UploadProfileImageAsync(userId!, viewModel.ProfileImage);

    //        return RedirectToAction("Details");


    //    }

    //    ViewBag.Error = "Valideringsfel";
    //    return RedirectToAction("Details");
    //}
    //#endregion



   


    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
        var viewModel = new SavedCoursesViewModel();
        try
        {
            
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Index", "Default");
            }
            viewModel.Courses = _mapper.Map<IEnumerable<CourseModel>>(await _userService.GetSavedCourseAsync(userId));


            //var userId = _userManager.GetUserId(User);
            //if (userId == null)
            //{
            //    return View(viewModel);
            //}
            //var savedCourses = await _userService.GetSavedCourseAsync(userId);

            //var content = new StringContent(JsonConvert.SerializeObject(savedCourses.ContentResult), Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync(_configuration["ApiUris:CoursesByIds"], content);
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync());
            //    viewModel.Courses = result ?? [];
            //    return View(viewModel);

            //}
        }
        catch (Exception)
        {
            //logger
        }
        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> SaveCourse(string courseId)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Index", "Default");
            }
            var result = await _userService.SaveCourseAsync(userId, courseId);
            switch (result.StatusCode)
            {
                case ResultStatus.OK:
                    TempData["Success"] = "Course have been saved!";
                    break;
                case ResultStatus.EXISTS:
                    TempData["Warning"] = "Course is already saved";
                    break;
                default:
                    TempData["Error"] = "Something went wrong. Please try again!";
                    break;
            }
        }
        catch (Exception)
        {
            //logger
            return StatusCode(500);
        }
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> RemoveOneCourse(string courseId)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Index", "Default");
            }
            var result = await _userService.DeleteOneCourseAsync(userId, courseId);
            if (result.StatusCode == ResultStatus.OK)
            {
                TempData["Success"] = "Course have been removed";
                
            }
            else
            {
                TempData["Error"] = "Something went wrong. Please try again!";
            }
        }
        catch (Exception)
        {
            //logger
            TempData["Error"] = "An unexpected error occurred. Please try again later!";
        }
        return Ok();
    }
}