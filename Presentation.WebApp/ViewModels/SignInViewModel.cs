using Presentation.WebApp.Models;

namespace Presentation.WebApp.ViewModels;

public class SignInViewModel
{
    public SignInModel SignIn { get; set; } = new SignInModel();
    public string? ErrorMessage { get; set; }
}
