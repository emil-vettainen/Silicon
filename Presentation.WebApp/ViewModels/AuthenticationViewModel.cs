using Presentation.WebApp.Models;

namespace Presentation.WebApp.ViewModels;

public class AuthenticationViewModel
{
    public SignUpModel SignUp { get; set; } = new SignUpModel();

    public SignInModel SignIn { get; set; } = new SignInModel();

}
