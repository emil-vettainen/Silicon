using Business.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Shared.Utilis;

namespace Business.Services;

public class ProfileService
{
    private readonly ProfileRepository _profileRepository;
    private readonly ErrorLogger _errorLogger;

    public ProfileService(ProfileRepository profileRepository, ErrorLogger errorLogger)
    {
        _profileRepository = profileRepository;
        _errorLogger = errorLogger;
    }


    public async Task<ProfileEntity> CreateProfileEntityAsync(ProfileDto dto, Guid AccountId)
    {
        try
        {
            var result = await _profileRepository.CreateAsync(new ProfileEntity
            {
                AccountId = AccountId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            });
            return result;
        }
        catch (Exception)
        {

           
        }
        return null!;
    }


    public async Task<bool> UploadProfileImageAsync(Guid AccountId, IFormFile profileImage)
    {
        try
        {
            var imagePath = await SaveImageToFileAsync(profileImage);

            var profile = await _profileRepository.GetOneAsync(x => x.AccountId == AccountId);
            if(profile != null)
            {
                profile.ProfileImageUrl = imagePath;
                var result = await _profileRepository.UpdateAsync(x => x.AccountId == AccountId, profile);
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


    public async Task<bool> UpdateProfileEntityAsync(Guid AccountId, string firstName, string lastName, string? biography)
    {
        try
        {
            var newProfileEntity = await _profileRepository.UpdateAsync(x => x.AccountId == AccountId, new ProfileEntity
            {
                AccountId = AccountId,
                FirstName = firstName,
                LastName = lastName,
                Biography = biography
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
