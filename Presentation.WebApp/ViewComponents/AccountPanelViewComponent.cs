using Business.Services;
using Infrastructure.Entities.AccountEntites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;
using Presentation.WebApp.ViewModels.Account;

namespace Presentation.WebApp.ViewComponents;

public class AccountPanelViewComponent : ViewComponent
{
 
    private readonly UserManager<UserEntity> _userManager;


    public AccountPanelViewComponent(UserManager<UserEntity> userManager)
    {

        _userManager = userManager;
    
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(UserClaimsPrincipal);

        if (user == null)
        {
            return View();
        }

        var viewModel = new AccountPanelViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfileImageUrl = user.ProfileImageUrl,
            Email = user.Email!,
        };
        return View(viewModel);
    }
}