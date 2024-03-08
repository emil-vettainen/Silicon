using Business.Dtos;
using Business.Factories;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Presentation.WebApp.Models;
using Presentation.WebApp.ViewModels;
using Shared.Responses.Enums;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly ProfileService _profileService;
    private readonly UserService _userService;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IModelStateService _modelStateService;

    public AccountController(ProfileService profileService, UserManager<UserEntity> userManager, UserService userService, IModelStateService modelStateService)
    {
        _profileService = profileService;
        _userManager = userManager;
        _userService = userService;
        _modelStateService = modelStateService;
    }

    #region [HttpGet] /account/details
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var userId = _userManager.GetUserId(User);
        var userInfo = await _userService.GetBasicInfoAsync(userId!);
        //var addressInfo = await _addressService.GetAddresInfoAsync(userId!);
       
        var viewModel = new AccountDetailsViewModel
        {
            BasicInfo = new BasicInfoModel { FirstName = userInfo.FirstName, LastName = userInfo.LastName, Email = userInfo.Email, Phone = userInfo.PhoneNumber, Biography = userInfo.Biography},
            AddressInfo = new AddressInfoModel { Addressline_1 = "Skara", PostalCode = "12345", City = "Skara" }
        };

        return View(viewModel);
    }
    #endregion

    #region [HttpPost] /account/details
    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel, string action)
    {
        var userId = _userManager.GetUserId(User);
        //var user = await _userManager.FindByIdAsync(userId!);
        var userInfo = await _userService.GetBasicInfoAsync(userId!);

        switch (action)
        {
            case "basicinfo":
                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
                {
                    var response = await _userService.UpdateUserAsync(userId!, UserFactory.UpdateUserDto(viewModel.BasicInfo.FirstName, viewModel.BasicInfo.LastName, viewModel.BasicInfo.Email, viewModel.BasicInfo.Phone!, viewModel.BasicInfo.Biography!));
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
                if(viewModel.AddressInfo.Addressline_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {
                    // uppdatera
                    // hantera response
              
                }
                break;
        }

        viewModel.AddressInfo ??= new AddressInfoModel { Addressline_1 = "Skara", PostalCode = "12345", City = "Skara" };
        viewModel.BasicInfo ??= new BasicInfoModel { FirstName = userInfo.FirstName, LastName = userInfo.LastName, Email = userInfo.Email, Phone = userInfo.PhoneNumber, Biography = userInfo.Biography };
        
        return View(viewModel);
    }
    #endregion







    public IActionResult Security()
    {
        return View();
    }


    [HttpPost]
    public async Task <IActionResult> UpdateProfileImage(AccountPanelViewModel viewModel)
    {
        if (viewModel.ProfileImage != null)
        {

         

            var userId = _userManager.GetUserId(User);
            if(userId != null)
            {
                await _userService.UploadProfileImageAsync(userId, viewModel.ProfileImage);
            }

            


            //var result = await _profileService.UploadProfileImageAsync(userId!,viewModel.ProfileImage);
           
        }
       
        return RedirectToAction("Details");
    }
}
