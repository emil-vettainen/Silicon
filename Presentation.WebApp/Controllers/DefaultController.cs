using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Default;
using System.Text;

namespace Presentation.WebApp.Controllers;

public class DefaultController : Controller
{
    private readonly HttpClient _httpClient;

    public DefaultController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Route("/")]
    public IActionResult Home()
    {
        var viewModel = new HomeViewModel();
        return View(viewModel);
    }

    [Route("/")]
    [HttpPost]
    public async Task<IActionResult> Subscribe(HomeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel.SubscribeModel), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7011/api/subscribers", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "You have been subscribed";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["Warning"] = "You are already a subscriber";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["Error"] = "Something went wrong, Please contact web admin.";
                }
            }
            catch (Exception)
            {
                //logger
                TempData["Erorr"] = "An unexpected error occurred. Please try again!";
            }
        }
        else
        {
            TempData["Warning"] = "Invalid email address! The Email must match xx@xx.xx";
        }
        return View("Home", viewModel);
    }



    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        return View();
    }
}
