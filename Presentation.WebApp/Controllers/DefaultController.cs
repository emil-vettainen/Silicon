using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.WebApp.ViewModels.Default;
using System.Diagnostics;
using System.Text;

namespace Presentation.WebApp.Controllers;

public class DefaultController(HttpClient httpClient, IConfiguration configuration) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    #region /Home
    [Route("/")]
    public IActionResult Home()
    {
        ViewData["Title"] = "Home";
        var viewModel = new HomeViewModel();
        return View(viewModel);
    }
    #endregion


    #region Subscribe
    [HttpPost]
    public async Task<IActionResult> Subscribe(HomeViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(viewModel.SubscribeModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiUris:Subscribers"]}?key={_configuration["Api:Key"]}", content);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return Conflict();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Unauthorized();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return StatusCode(500);
    }
    #endregion


    #region Not Found
    [Route("/error")]
    public IActionResult Error404(int statusCode)
    {
        return View();
    }
    #endregion


    #region Denied
    [Route("/denied")]
    public IActionResult Error401()
    {
        return View();
    }
    #endregion


}