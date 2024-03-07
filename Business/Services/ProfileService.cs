using Business.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Utilis;
using System.Linq.Expressions;

namespace Business.Services;

public class ProfileService
{
    private readonly ProfileRepository _profileRepository;
    private readonly ErrorLogger _errorLogger;

    private readonly UserManager<UserEntity> _userManager;


    public ProfileService(ProfileRepository profileRepository, ErrorLogger errorLogger, UserManager<UserEntity> userManager)
    {
        _profileRepository = profileRepository;
        _errorLogger = errorLogger;
        _userManager = userManager;
    }



    public async Task<ProfileDto> PopulateViewModel(string userId, string action)
    {
        try
        {
            var profile = await GetOneProfileAsync(x => x.UserId == userId);
            var user = await _userManager.FindByIdAsync(userId);

            var dto = new ProfileDto();
            switch(action)
            {
                case "BaseInfo":

                    dto.UserId = userId;
                    dto.FirstName = profile.FirstName;
                    dto.LastName = profile.LastName;
                    dto.Biography = profile.Biography;
                    dto.ProfileImageUrl = profile.ProfileImageUrl;
                    dto.Email = user.Email;
                    dto.PhoneNumber = user.PhoneNumber;

                        break;

                case "AddressInfo":
                    break;

            }

            return dto;


        }
        catch (Exception)
        {

        }
        return null!;
    }

 


    public async Task<ProfileEntity> GetOneProfileAsync(Expression<Func<ProfileEntity, bool>> predicate)
    {
        try
        {
            var profile = await _profileRepository.GetOneAsync(predicate);
            if(profile != null)
            {
                return profile;
            }

        }
        catch (Exception)
        {

            
        }
        return null!;
    }


 

    public async Task<bool> CreateProfileEntityAsync(ProfileDto dto)
    {
        try
        {
            var result = await _profileRepository.CreateAsync(new ProfileEntity
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            });
            return result != null;
        }
        catch (Exception)
        {

           
        }
        return false;
    }


    public async Task<bool> UploadProfileImageAsync(string UserId, IFormFile profileImage)
    {
        try
        {
            var imagePath = await SaveImageToFileAsync(profileImage);

            var profile = await _profileRepository.GetOneAsync(x => x.UserId == UserId);
            if(profile != null)
            {
                profile.ProfileImageUrl = imagePath;
                var result = await _profileRepository.UpdateAsync(x => x.UserId == UserId, profile);
                if(result != null)
                {
                    return true;
                }
            }
            return false;


        }
        catch (Exception)
        {

           
        }
        return false;
    }


    public async Task<bool> UpdateProfileEntityAsync(string userId, ProfileDto dto)
    {
        try
        {
            var newProfileEntity = await _profileRepository.UpdateAsync(x => x.UserId == userId, new ProfileEntity
            {
                UserId = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Biography = dto.Biography
            });
            return newProfileEntity != null;
            
        }
        catch (Exception)
        {

           
        }
        return false;
    }


    public async Task<string> SaveImageToFileAsync(IFormFile profileImage)
    {
        try
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(fileStream);
            }
            return $"/images/uploads/{fileName}";
        }
        catch (Exception)
        {

        }
        return null!;

    }

}
