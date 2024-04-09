using Business.Factories;
using Business.Services;
using Infrastructure.Entities.AccountEntites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels.Authentication;
using Shared.Responses.Enums;


namespace Presentation.WebApp.Controllers;

public class AuthenticationController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;   
    private readonly UserService _userService;
    private readonly HttpClient _httpClient;


    public AuthenticationController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserService userService, HttpClient httpClient)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        _httpClient = httpClient;
    }

    #region SignUp
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
        var result = await _userService.CreateAsync(UserFactory.ToDto(viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.Password));
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


    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string? returnUrl)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Incorret email or password";
                return View(viewModel);
            }
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null && await _userManager.IsInRoleAsync(user, "admin"))
            {
                if (!await _userService.GetToken())
                {
                    TempData["Error"] = "Access token failed!";
                    return View(viewModel);
                }
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData["Error"] = "An unexpected error occurred, Please try again!";
            return View(viewModel);
        }
    }
    #endregion


    #region SignOut
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Default");
    }
    #endregion


    #region External Authentication
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("ExternalCallback"));
        return new ChallengeResult("Google", authProps);
    }

    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("ExternalCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    public async Task<IActionResult> ExternalCallback()
    {
        var result = await _userService.HandleExternalLoginAsync();
        switch (result.StatusCode)
        {
            case ResultStatus.OK: 
                if (HttpContext.User != null)
                {
                    return RedirectToAction("Details", "Account");
                }
                else
                {
                    ViewBag.Error = "Authentication failed. Please try again.";
                    return RedirectToAction("SignIn", "Authentication");
                }
            default:
                ViewBag.Error = "An unexpected error occurred.";
                break;
        }
        return RedirectToAction("SignIn", "Authentication");
    }
    #endregion
}