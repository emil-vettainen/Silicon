using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AuthenticationController : Controller
{
    

    [Route("/register")]
    public IActionResult SignUp()
    {
        var viewModel = new SignUpModel();
        return View(viewModel);
    }

    [Route("/register")]
    [HttpPost]
    public IActionResult SignUp(SignUpModel viewModel)
    {
        if(ModelState.IsValid)
        {
            // registrera 

            return RedirectToAction("Index", "Home");
        }

        return View(viewModel);
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
    public IActionResult SignIn(SignInViewModel viewModel)
    {

        if(!ModelState.IsValid)
        {
            return View(viewModel);

            
            
        }

        // om ok från service return RedirectToAction

        viewModel.ErrorMessage = "Incorrect email or password";

        return View(viewModel);
    }




}
