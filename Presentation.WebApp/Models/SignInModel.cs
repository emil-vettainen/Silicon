using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models;

public class SignInModel
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [Required(ErrorMessage = "Email address is required")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password", Order = 2)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember Me", Order = 3)]
    public bool RememberMe { get; set; } = false;
}