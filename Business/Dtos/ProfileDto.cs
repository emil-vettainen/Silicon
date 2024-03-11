using Microsoft.AspNetCore.Http;

namespace Business.Dtos;

public class ProfileDto
{
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
