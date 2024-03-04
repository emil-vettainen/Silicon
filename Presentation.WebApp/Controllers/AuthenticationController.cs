
using Business.Dtos;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AuthenticationController : Controller
{
    private readonly UserManager<AccountEntity> _userManager;
    private readonly SignInManager<AccountEntity> _signInManager;   
    private readonly ProfileService _profileService;

    public AuthenticationController(Microsoft.AspNetCore.Identity.UserManager<AccountEntity> userManager, ProfileService profileService, SignInManager<AccountEntity> signInManager)
    {
        _userManager = userManager;
        _profileService = profileService;
        _signInManager = signInManager;
    }

    [Route("/register")]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }


    [Route("/register")]
    [HttpPost]
    public async Task <IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var user = new AccountEntity { UserName = viewModel.SignUp.Email, Email = viewModel.SignUp.Email };
        var result = await _userManager.CreateAsync(user, viewModel.SignUp.Password);

        if(!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Code == "DuplicateUserName" ? "Email already exists." : error.Description);
            }
            return View(viewModel);
        }

        var createProfile = await _profileService.CreateProfileEntityAsync(new ProfileDto
        {
            UserId = user.Id,
            FirstName = viewModel.SignUp.FirstName,
            LastName = viewModel.SignUp.LastName,
        });

        if(!createProfile)
        {
            ModelState.AddModelError("", "Something went wrong, please try again");
            return View(viewModel); 
        }

        return RedirectToAction(nameof(SignIn));
    }


    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        var viewModel = new SignInViewModel();
        return View(viewModel);  
    }



    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {

        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var result = await _signInManager.PasswordSignInAsync(viewModel.SignIn.Email, viewModel.SignIn.Password, viewModel.SignIn.RememberMe, false);

        if(!result.Succeeded)
        {
            ModelState.AddModelError("", "Incorrect email or password");
            return View(viewModel);
        }

        return RedirectToAction("Index", "Home");

    }


}
