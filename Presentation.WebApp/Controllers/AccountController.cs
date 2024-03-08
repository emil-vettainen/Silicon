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

    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var basicinfoErrors = _modelStateService.LoadModelState("BasicInfoErrors");
        if (basicinfoErrors.Count > 0 )
        {
            ModelState.Merge(basicinfoErrors);
        }



        var viewModel = new AccountDetailsViewModel
        {
            BasicInfo = await PopulateBaseInfoAsync(),
            AddressInfo = new AddressInfoModel
            {
                Addressline_1 = "Skara",
                PostalCode = "12345",
                City = "Skara"
            }
        };

   

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult UpdateBasicInfo(BasicInfoModel viewModel)
    {
        var key = "BasicInfoErrors";

        if(!ModelState.IsValid)
        {
            

            return RedirectToAction("Details");
        }
        else
        {
            
            _modelStateService.ClearModelState(key);
        }

        return RedirectToAction("Details");

    }

    [HttpPost]
    [Route("/account/details")]
    public IActionResult Details(AccountDetailsViewModel viewModel, string action)
    {
        switch (action)
        {
            case "basicinfo":
                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
                {
                    // uppdatera
                    //
                    //
                }
                break;

            case "addressinfo":
                if(viewModel.AddressInfo.Addressline_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {
                    // uppdatera
                    //
                    //
                }

                break;

        }

        if (viewModel.AddressInfo == null)
        {
            viewModel.AddressInfo = new AddressInfoModel
            {
                Addressline_1 = "Skara",
                PostalCode = "12345",
                City = "Skara"

            };
        }

        if(viewModel.BasicInfo == null)
        {
            viewModel.BasicInfo = new BasicInfoModel
            {
                FirstName = "Emil",
                LastName = "12345",
                Email = "12345",
            };
        }


        return View(viewModel);



    }


    private async Task<BasicInfoModel> PopulateBaseInfoAsync()
    {
        try
        {
            var userEntity = await _userManager.FindByIdAsync(_userManager.GetUserId(User)!);
            if (userEntity != null)
            {
                var model = new BasicInfoModel
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Biography = userEntity.Biography,
                    Email = userEntity.Email!,
                    Phone = userEntity.PhoneNumber,
                };
                return model;
            }
        }
        catch (Exception)
        {

        }
        return null!;
    }




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
