using Business.Services;
using Infrastructure.Entities.AccountEntites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
  
    public HomeController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel();

        //var user = await _userManager.GetUserAsync(User);
        //if (user != null)
        //{
        //    var profile = await _profileService.GetOneProfileAsync(x => x.UserId == user.Id);
        //    if (profile != null)
        //    {
        //        viewModel.BaseInfo.FirstName = profile.FirstName;
        //        viewModel.BaseInfo.LastName = profile.LastName;

        //        return View(viewModel);
        //    }
        //}



        return View(viewModel);
    }
}
