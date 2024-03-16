using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels.Contact;

namespace Presentation.WebApp.Controllers;

public class ContactController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }



    [HttpPost]
    public IActionResult Contact(ContactUsViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return View(viewModel);

    }
}
