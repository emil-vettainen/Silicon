﻿using Microsoft.AspNetCore.Http;

namespace Business.Dtos;

public class ProfileDto
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public string? ProfileImageUrl { get; set; }
}
