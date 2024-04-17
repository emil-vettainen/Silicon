using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Contact;
using System.Diagnostics;
using System.Text;

namespace Presentation.WebApp.Controllers;

public class ContactController(HttpClient httpClient, IConfiguration configuration) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;


    #region Contact Request
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
            var response = await _httpClient.PostAsync($"{_configuration["ApiUris:Contact"]}?key={_configuration["Api:Key"]}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Your request was sent successfully, we will get back to you as soon as possible!";
                return RedirectToAction("Contact");
            }
            else
            {
                TempData["Error"] = "Something went wrong. Please try again!";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            TempData["Error"] = "An unexpected error occurred, Please contact support!";
        }
        return View();
    }
    #endregion


}