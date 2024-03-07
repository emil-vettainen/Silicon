using Shared.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.ViewModels.Account;

public class DeleteAccountViewModel
{

    [Display(Name = "Current password", Prompt = "Enter your current password", Order = 0)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; set; } = null!;


    [Display(Name = "New password", Prompt = "Enter your new password", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, numeric, and special characters.")]
    public string NewPassword { get; set; } = null!;


    [Display(Name = "Confirm new password", Prompt = "Confirm your new password", Order = 2)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please Confirm your password")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "Yes, I want to delete my account", Order = 3)]
    [CheckBoxRequired(ErrorMessage = "You must confirm")]
    public bool DeletePassword { get; set; } = false;
}