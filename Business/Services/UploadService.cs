using Infrastructure.Entities.AccountEntites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;


namespace Business.Services;

public class UploadService(UserManager<UserEntity> userManager, IConfiguration configuration)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;


    public async Task<bool> UploadProfileImageAsync(ClaimsPrincipal user, IFormFile file)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(user);
            if (userEntity == null) return false;

            var fileName = await SaveFileAsync(file, "profiles-img");
            if (string.IsNullOrEmpty(fileName)) return false;

            userEntity.ProfileImageUrl = fileName;
            var result = await _userManager.UpdateAsync(userEntity);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


    public async Task<string> SaveFileAsync(IFormFile file, string folder)
    {
        try
        {
            if (file != null && file.Length >0)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"{_configuration["FileUploadPath"]}",folder);
                Directory.CreateDirectory(uploadsFolderPath);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return string.Empty;
    }


}