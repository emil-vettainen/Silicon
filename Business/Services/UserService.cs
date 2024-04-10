using Business.Dtos;
using Business.Dtos.Course;
using Business.Dtos.User;
using Business.Factories;
using Business.Services;
using Infrastructure.Entities.AccountEntites;
using Infrastructure.Entities.AccountEntities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.SqlRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Factories;
using Shared.Responses;
using Shared.Responses.Enums;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Services;

public class UserService
{

    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserAddressRepository _userAddressRepository;
    private readonly SavedCourseRepository _savedCourseRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserAddressRepository userAddressRepository, IConfiguration config, SavedCourseRepository savedCourseRepository, HttpClient httpClient, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userAddressRepository = userAddressRepository;
        _configuration = config;
        _savedCourseRepository = savedCourseRepository;
        _httpClient = httpClient;
        _contextAccessor = contextAccessor;
    }


    public async Task<UserAddressEntity> GetAddressInfoAsync(string userId)
    {

        var result = await _userAddressRepository.GetAllAddressesAsync(userId);
        return result;

    }




    public async Task<ResponseResult> HandleExternalLoginAsync()
    {
        try
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return ResponseFactory.Error();
            }

            //userFromExternal = 
            var userEntity = UserFactory.CreateUserEntity(info.Principal.FindFirstValue(ClaimTypes.GivenName)!, info.Principal.FindFirstValue(ClaimTypes.Surname)!, info.Principal.FindFirstValue(ClaimTypes.Email)!, true);
            var user = await _userManager.FindByEmailAsync(userEntity.Email!);

            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (!result.Succeeded)
                {

                    return ResponseFactory.Error();

                }
                else
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email!);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return ResponseFactory.Ok();
                }
               
            }

      

            else
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;
                  
                    await _userManager.UpdateAsync(user);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

             

                return ResponseFactory.Ok();

            }
             

        }
        catch (Exception)
        {
            return ResponseFactory.Error();

        }
       

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
        catch (Exception)
        {

            return ResponseFactory.Error();
            
        }

      


    }
   



    public async Task<GetUserDto> GetBasicInfoAsync(string userId)
    {
        try
        {
          
            
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return null!;
                }
                return UserFactory.GetUserDto(user.FirstName, user.LastName, user.Email!, user.PhoneNumber!, user.Biography!, user.IsExternalAccount);
            

            

        }
        catch (Exception)
        {

            throw;
        }
    }



    public async Task<ResponseResult> UpdateUserAsync(string userId, UpdateUserDto dto)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return ResponseFactory.NotFound();
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Biography = dto.Biography;
            user.IsExternalAccount = dto.IsExternalAccount;

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return ResponseFactory.Error();
            }
            return ResponseFactory.Ok();
        }
        catch (Exception)
        {

            return ResponseFactory.Error();
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




    public async Task<bool> UploadProfileImageAsync(ClaimsPrincipal user, IFormFile profileImage)
    {
		try
		{
            var userEntity = await _userManager.GetUserAsync(user);
            if (userEntity == null) return false;

            var fileName = await SaveImageToFileAsync(userEntity.Id, profileImage);
            if (string.IsNullOrEmpty(fileName)) return false;

            userEntity.ProfileImageUrl = fileName;
            var result = await _userManager.UpdateAsync(userEntity);
            return result.Succeeded;
		}
		catch (Exception)
		{
            //logger
            return false;
        }
    }


    private async Task<string> SaveImageToFileAsync(string userId, IFormFile profileImage)
    {
        try
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!);
            Directory.CreateDirectory(uploadsFolderPath);

            var fileName = $"p_{userId}_{Guid.NewGuid()}{Path.GetExtension(profileImage.FileName)}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profileImage.CopyToAsync(fileStream);
            }
            return fileName;
        }
        catch (Exception)
        {
            //logger
            return string.Empty;
        }
    }



    public async Task<ResponseResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var pattern = @"^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) 
        {
            return ResponseFactory.NotFound();
        }

        if(!Regex.IsMatch(newPassword, pattern))
        {
            return ResponseFactory.Error("Invalid password");
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            return ResponseFactory.Error("An unexpected error occurred.");
        }

        return ResponseFactory.Ok();

    }



    public async Task<ResponseResult> SaveCourseAsync(string userId, string courseId)
    {
        try
        {
            var existingCourse = await _savedCourseRepository.ExistsAsync(x => x.UserId == userId && x.CourseId == courseId);
            if (existingCourse)
            {
                return ResponseFactory.Exists();
            }
            var result = await _savedCourseRepository.CreateAsync(new SavedCourseEntity {UserId = userId, CourseId = courseId});
            return result != null ? ResponseFactory.Ok() : ResponseFactory.Error();
        }
        catch (Exception)
        {
            //logger
            return ResponseFactory.Error();
        }
    }


    public async Task<IEnumerable<CourseDto>> GetSavedCourseAsync(string userId)
    {
        try
        {
            var savedCourses = await _savedCourseRepository.GetSavedCoursesAsync(userId);
            if (!savedCourses.Any())
            {
                return [];
            }
            var content = new StringContent(JsonConvert.SerializeObject(savedCourses), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiUris:CoursesByIds"]}?key={_configuration["Api:Key"]}", content);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(await response.Content.ReadAsStringAsync());
                return result ?? [];
            }
        }
        catch (Exception)
        {
            //logger

        }
        return [];
    }


    public async Task<ResponseResult> DeleteOneCourseAsync(string userId, string courseId)
    {
        try
        {
            var response = await _savedCourseRepository.DeleteAsync(x => x.UserId == userId && x.CourseId == courseId);
            return response ? ResponseFactory.Ok() : ResponseFactory.NotFound();
        }
        catch (Exception)
        {

            return ResponseFactory.Error();
        }

    }


    public async Task<bool> GetToken()
    {
        try
        {
            var tokenResponse = await _httpClient.SendAsync(new HttpRequestMessage 
            { 
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_configuration["Api:Token"]}?key={_configuration["Api:Key"]}"),
            });
            if (tokenResponse.IsSuccessStatusCode)
            {
                var token = await tokenResponse.Content.ReadAsStringAsync();
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.Now.AddDays(1)
                };
                _contextAccessor.HttpContext!.Response.Cookies.Append("AccessToken", token, cookieOptions);
                return true;
            }
        }
        catch (Exception)
        {
            //logger
        }
        return false;
    }
}
