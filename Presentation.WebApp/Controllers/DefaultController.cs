using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class DefaultController : Controller
{
    [Route("/")]
    public IActionResult Home()
    {
        var viewModel = new HomeViewModel();
        return View(viewModel);
    }

    [Route("/")]
    [HttpPost]
    public IActionResult Home(HomeViewModel viewModel)
    {
       
        return View(viewModel);
    }



    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        return View();
    }
}
