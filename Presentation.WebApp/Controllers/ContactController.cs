using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class ContactController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }
}
