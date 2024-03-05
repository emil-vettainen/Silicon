using Business.Dtos;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly ProfileService _profileService;
    private readonly UserManager<AccountEntity> _userManager;

    public AccountController(ProfileService profileService, UserManager<AccountEntity> userManager)
    {
        _profileService = profileService;
        _userManager = userManager;
    }

    public  async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel();

        var userId = _userManager.GetUserId(User);
        if(userId != null)
        {
            var accountEntity = await _userManager.FindByIdAsync(userId);

            viewModel.BaseInfo.Email = accountEntity.Email;
            viewModel.BaseInfo.Phone = accountEntity.PhoneNumber;

            var profile = await _profileService.GetOneProfileAsync(x => x.UserId == userId);

            viewModel.BaseInfo.FirstName = profile.FirstName;
            viewModel.BaseInfo.LastName = profile.LastName;



            return View(viewModel);
        }

        return View(viewModel);
      
    }


    [HttpPost]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        ModelState.Clear();
        if (TryValidateModel(viewModel.BaseInfo, nameof(viewModel.BaseInfo)))
        {
            var userId = _userManager.GetUserId(User);

            

            await _profileService.UpdateProfileEntityAsync(userId, new ProfileDto
            {
                FirstName = viewModel.BaseInfo.FirstName,
                LastName = viewModel.BaseInfo.LastName,
                Biography = viewModel.BaseInfo.Biography,

            });
        }

        return View(viewModel);
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


            var result = await _profileService.UploadProfileImageAsync(userId!,viewModel.ProfileImage);
           
        }
       
        return RedirectToAction("Details");
    }
}
