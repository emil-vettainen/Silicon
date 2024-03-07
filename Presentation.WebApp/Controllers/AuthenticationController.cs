using Business.Factories;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using Shared.Responses.Enums;


namespace Presentation.WebApp.Controllers;

public class AuthenticationController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;   
    private readonly UserService _userService;


    public AuthenticationController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
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
        var result = await _userService.CreateAsync(UserFactory.CreateUser(viewModel.SignUp.FirstName, viewModel.SignUp.LastName, viewModel.SignUp.Email, viewModel.SignUp.Password));
        switch (result.StatusCode)
        {
            case ResultStatus.EXISTS:
                ModelState.AddModelError("", "Email is already exists");
                return View(viewModel);

            case ResultStatus.OK:
                return RedirectToAction(nameof(SignIn));

            default:
                ModelState.AddModelError("", result.Message!);
                return View(viewModel); 
        }
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


    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


}
