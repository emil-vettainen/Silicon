namespace Business.Dtos.User;

public class GetUserDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Biography { get; set; }
    public string? ProfileImageUrl { get; set; }
}
