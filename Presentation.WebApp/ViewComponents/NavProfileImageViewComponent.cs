using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ViewModels.Account;

namespace Presentation.WebApp.ViewComponents
{
    public class NavProfileImageViewComponent : ViewComponent
    {
        private readonly UserManager<UserEntity> _userManager;

        public NavProfileImageViewComponent(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
            if(user != null)
            {
                var profileImage = new ProfileImageViewModel
                {
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                return View(profileImage);
            }
            return View();
        }
    }
}