using Business.Services;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels;

namespace Presentation.WebApp.ViewComponents;

public class AccountPanelViewComponent : ViewComponent
{
    private readonly ProfileService _profileService;
    private readonly UserManager<AccountEntity> _userManager;

    public AccountPanelViewComponent(ProfileService profileService, UserManager<AccountEntity> userManager)
    {
        _profileService = profileService;
        _userManager = userManager;
    }


    public async Task<IViewComponentResult> InvokeAsync()
    {

        var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
        

        var profileEntity = await _profileService.GetOneProfileAsync(x => x.UserId == user!.Id);

        var viewModel = new AccountPanelViewModel
        {
           
            FirstName = profileEntity.FirstName,
            LastName = profileEntity.LastName,
            ProfileImageUrl = profileEntity.ProfileImageUrl,
            Email = user.Email
        };

        return View(viewModel);

    }

}