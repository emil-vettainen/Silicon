using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class AuthenticationController : Controller
{

    [Route("/register")]
    public IActionResult SignUp()
    {
        var viewModel = new AuthenticationViewModel();
        return View(viewModel);
    }

    [Route("/register")]
    [HttpPost]
    public IActionResult SignUp(AuthenticationViewModel viewModel)
    {
        if(ModelState.IsValid)
        {
            // registrera 

            return RedirectToAction("Index", "Home");
        }

        return View(viewModel);
    }


}
