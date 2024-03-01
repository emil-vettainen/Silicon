using Presentation.WebApp.Models;

namespace Presentation.WebApp.ViewModels;

public class SignUpViewModel
{
    public SignUpModel SignUp { get; set; } = new SignUpModel();
    public string? ErrorMessage { get; set; }
}
