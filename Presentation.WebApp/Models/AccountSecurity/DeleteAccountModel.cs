using Shared.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models.AccountSecurity;

public class DeleteAccountModel
{
    
    [Display(Name = "Yes, I want to delete my account")]
    [CheckBoxRequired(ErrorMessage = "You must confirm!")]
    [Required(ErrorMessage = "You must confirm!")]
    public bool DeleteAccount { get; set; } = false;
}