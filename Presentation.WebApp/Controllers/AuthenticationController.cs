using Business.Factories;
using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using Shared.Responses.Enums;
using System.Security.Claims;


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
    

  
    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string? returnUrl)
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
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Default");
    }
    #endregion



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




        //if (result.StatusCode == ResultStatus.OK)
        //{
        //    if (HttpContext.User != null)
        //    {
        //        return RedirectToAction("Details", "Account");
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Failed to authenticate";
        //        return RedirectToAction("SignIn", "Authentication");
        //    }
        //}
        //else
        //{
        //    ViewBag.Error = result.Message ?? "Failed to authenticate";
        //    return RedirectToAction("SignIn", "Authentication");
        //}


        //var info = await _signInManager.GetExternalLoginInfoAsync();

        //if (info != null)
        //{
        //    var userEntity = new UserEntity
        //    {
        //        FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
        //        LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
        //        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
        //        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
        //    };

        //    var user = await _userManager.FindByEmailAsync(userEntity.Email);


        //    if (user == null)
        //    {
        //        var result = await _userManager.CreateAsync(userEntity);
        //        if (result.Succeeded)
        //        {
        //            user = await _userManager.FindByEmailAsync(userEntity.Email);
        //        }
        //    }


        //    // exists 

        //    if (user != null)
        //    {
        //        if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email )
        //        {
        //            user.FirstName = userEntity.FirstName;
        //            user.LastName = userEntity.LastName;
        //            user.Email = userEntity.Email;

        //            await _userManager.UpdateAsync(user);
        //        }

        //        await _signInManager.SignInAsync(user, isPersistent: false);

        //        if (HttpContext.User != null)
        //        {
        //            return RedirectToAction("Details", "Account");
        //        }
        //    }
        //}

        //ViewBag.Error = "Failed to authenticate with Facebook ";
        //return RedirectToAction("SignIn", "Authenticaction");
    }

   
}