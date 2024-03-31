using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Contact;
using System.Text;

namespace Presentation.WebApp.Controllers;

public class ContactController : Controller
{
    private readonly HttpClient _httpClient;

    public ContactController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Route("/contact")]
    [HttpGet]
    public IActionResult Contact()
    {
        var viewModel = new ContactUsViewModel();
        return View(viewModel);
    }

    [Route("/contact")]
    [HttpPost]
    public async Task <IActionResult> Contact(ContactUsViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        try
        {
            if (string.IsNullOrEmpty(viewModel.Service))
            {
                viewModel.Service = null;
            }
            var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7011/api/contactrequests", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Success = "Your request was sent successfully, we will get back to you as soon as possible!";
            }
            else
            {
                ViewBag.Error = "Something went wrong. Please try again!";
            }
        }
        catch (Exception)
        {
            //logger
            ViewBag.Error = "The server encountered an unexpected condition";
        }
        return View();
    }
}
