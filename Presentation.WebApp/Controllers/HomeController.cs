using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<AccountEntity> _userManager;
    private readonly ProfileService _profileService;

    public HomeController(UserManager<AccountEntity> userManager, ProfileService profileService)
    {
        _userManager = userManager;
        _profileService = profileService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel();

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var profile = await _profileService.GetOneProfileAsync(x => x.UserId == user.Id);
            if (profile != null)
            {
                viewModel.BaseInfo.FirstName = profile.FirstName;
                viewModel.BaseInfo.LastName = profile.LastName;

                return View(viewModel);
            }
        }



        return View(viewModel);
    }
}
