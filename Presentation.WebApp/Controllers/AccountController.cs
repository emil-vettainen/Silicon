using Business.Dtos;
using Business.Factories;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    public AccountController(ProfileService profileService, UserManager<UserEntity> userManager, UserService userService)
    {
        _profileService = profileService;
        _userManager = userManager;
        _userService = userService;
    }


    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        if (TempData["ModelState"] is ModelStateDictionary modelState)
        {
            ModelState.Merge(modelState);

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


    public IActionResult UpdateBasicInfo (BasicInfoModel model)
    {
        if(!ModelState.IsValid)
        {
            // uppdatera
            TempData["ModelState"] = ModelState;

            return RedirectToAction("Details");
        }
        else
        {
            return RedirectToAction("Details");
        }
    }



    #region [HttpPost] Details

    [HttpPost]
    [Route("/account/details")]
    public async Task <IActionResult> Details(AccountDetailsViewModel viewModel, string action)
    {

        var userId = _userManager.GetUserId(User);

        //var profile = await _profileService.GetOneProfileAsync(x => x.UserId == user.Id);



        switch (action)
        {
            case "basic":

                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
                {
                    // uppdatera user
                }
                else
                {
                    viewModel.AddressInfo.Addressline_1 = "Skara";
                    viewModel.AddressInfo.PostalCode = "12345";
                    viewModel.AddressInfo.City = "Skara";
                   

                }

           

                break;

            case "address":

                if(viewModel.AddressInfo.Addressline_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {
                    //uppdatera address
                }
                else
                {
                    viewModel.BasicInfo = await PopulateBaseInfoAsync();
                }


                break;

        }



        return View(viewModel);
    }
    #endregion

    private async Task<BasicInfoModel> PopulateBaseInfoAsync()
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(HttpContext.User);
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

    //private async Task<AddressInfoModel> PopulateAddressInfoAsync()
    //{
    //    try
    //    {
    //        var userId = _userManager.GetUserId(User);
    //        var addressEntity = await _addressService.GetAddressAsync(userId);
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}




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
        }
        return RedirectToAction("Details");
    }
}
