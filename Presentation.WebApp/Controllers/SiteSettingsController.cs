using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class SiteSettingsController : Controller
{

    #region Theme
    public IActionResult ChangeTheme(string theme)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(60),
        };
        Response.Cookies.Append("ThemeMode", theme, option);
        return Ok();
    }
    #endregion


    #region Cookie Consent
    [HttpPost]
    public IActionResult CookieConsent()
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = true,
        };
        Response.Cookies.Append("CookieConsent", "true", option);
        return Ok();
    }
    #endregion


}