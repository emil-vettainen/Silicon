using Business.Dtos;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Shared.Factories;
using Shared.Responses;
using System.Runtime.CompilerServices;

namespace Business.Services;

public class UserService
{

    private readonly UserManager<UserEntity> _userManager;

    public UserService(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }


    public async Task<ResponseResult> CreateAsync(CreateUserDto dto)
    {
        try
        {
            var userExists = await _userManager.Users.AnyAsync(x => x.Email == dto.Email);
            if (userExists)
            {
                return ResponseFactory.Exists();
            }

            var createUser = await _userManager.CreateAsync(new UserEntity
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,

            }, dto.Password);


            if (!createUser.Succeeded)
            {
                return ResponseFactory.Error();
            }

            return ResponseFactory.Ok();
        }
        catch (Exception ex)
        {

            return ResponseFactory.Error();
            
        }

      


    }
   



    public async Task<bool> UpdateUserAsync(string userId, UpdateUserDto dto)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                // not found
            }

            var userExists = await _userManager.Users.AnyAsync(x => x.Email == user.Email);
            
            if (userExists)
            {
                return false;
            }
 

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Biography = dto.Biography;

            var result = await _userManager.UpdateAsync(user);


            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }



   



    public async Task<BaseInfoDto> PopulateBaseInfoAsync(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null!;
            }

            var model = new BaseInfoDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Biography = user.Biography,
                Email = user.Email!,
                Phone = user.PhoneNumber,
            };


            return model;





        }
        catch (Exception)
        {
            return null!;
        }
        
    }




    public async Task<bool> UploadProfileImageAsync(string userId,  IFormFile profileImage)
    {
		try
		{
            var imagePath = SaveImageToFileAsync(profileImage);


            
            var user = await _userManager.FindByIdAsync(userId);

            if(user != null)
            {
                user.ProfileImageUrl = await imagePath;
                await _userManager.UpdateAsync(user);
            }


            return true;
            
            
           

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
