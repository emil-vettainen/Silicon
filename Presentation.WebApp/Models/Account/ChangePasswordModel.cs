using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models.Account;

public class ChangePasswordModel
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
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;
}
