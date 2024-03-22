using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models.Home;

public class SubscribeModel
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 0)]
    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression(@"^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;


    public bool DailyNewsletter { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeenInReview { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool Podcasts { get; set; } = false;

}