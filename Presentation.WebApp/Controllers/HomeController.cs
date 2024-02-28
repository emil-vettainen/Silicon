using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewModel = new HomeViewModel();
        return View(viewModel);
    }
}
