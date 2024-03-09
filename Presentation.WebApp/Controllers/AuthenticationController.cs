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

    #region Register
    [Route("/register")]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpViewModel();
        return View(viewModel);
    }
    #endregion

    #region Register POST
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
                ViewBag.Error = "Email is already exists";
                return View(viewModel);

            case ResultStatus.OK:
                return RedirectToAction(nameof(SignIn));

            default:
                ViewBag.Error = result.Message;
                return View(viewModel); 
        }
    }
    #endregion

    #region SignIn
    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
        var viewModel = new SignInViewModel();
        return View(viewModel);  
    }
    #endregion

    #region SignIn POST
    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
    {
        if(!ModelState.IsValid)
        {
            return View(viewModel);
        }
        var result = await _signInManager.PasswordSignInAsync(viewModel.SignIn.Email, viewModel.SignIn.Password, viewModel.SignIn.RememberMe, false);
        if(!result.Succeeded)
        {
            ViewBag.Error = "Incorret email or password";
            return View(viewModel);
        }
        if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) 
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }
    #endregion

    #region SignOut
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion
}