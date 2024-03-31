using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.ViewModels.Contact;

public class ContactUsViewModel
{
    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression(@"^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Enter your message here", Order = 2)]
    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; } = null!;

}