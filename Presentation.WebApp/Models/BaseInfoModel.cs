namespace Presentation.WebApp.Models;

public class BaseInfoModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }   
    public string? ProfileImageUrl { get; set; }    
}
