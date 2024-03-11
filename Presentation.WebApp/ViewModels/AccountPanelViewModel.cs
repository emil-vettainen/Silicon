namespace Presentation.WebApp.ViewModels;

public class AccountPanelViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string Email { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public IFormFile? ProfileImage { get; set; }
}