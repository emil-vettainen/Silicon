using Business.Dtos.User;
using Business.Factories;
using Infrastructure.Entities.AccountEntites;
using Infrastructure.Entities.AccountEntities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Factories;
using Shared.Responses;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;



namespace Business.Services;

public class UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserAddressRepository userAddressRepository, IConfiguration config, SavedCourseRepository savedCourseRepository, HttpClient httpClient, IHttpContextAccessor contextAccessor)
{

    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserAddressRepository _userAddressRepository = userAddressRepository;
    private readonly SavedCourseRepository _savedCourseRepository = savedCourseRepository;
    private readonly IConfiguration _configuration = config;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;


    public async Task<UserAddressEntity> GetAddressInfoAsync(string userId)
    {
        try
        {
            var result = await _userAddressRepository.GetAllAddressesAsync(userId);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
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
                    await _signInManager.SignInAsync(user!, isPersistent: false);
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
        
    }


    public async Task<ResponseResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        try
        {
            var pattern = @"^(([^<>()\]\\.,;:\s@\""]+(\.[^<>()\]\\.,;:\s@\""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ResponseFactory.NotFound();
            }

            if (!Regex.IsMatch(newPassword, pattern))
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseFactory.Unavailable();
        }
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseFactory.Error();
        }
    }


    public async Task<IEnumerable<string>> GetSavedCourseAsync(string userId)
    {
        try
        {
            return await _savedCourseRepository.GetSavedCoursesAsync(userId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }


    public async Task<ResponseResult> DeleteOneCourseAsync(string userId, string courseId)
    {
        try
        {
            var response = await _savedCourseRepository.DeleteAsync(x => x.UserId == userId && x.CourseId == courseId);
            return response ? ResponseFactory.Ok() : ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseFactory.Error();
        }
    }


    public async Task<ResponseResult> DeleteAllCoursesAsync(string userId)
    {
        try
        {
            var result = await _savedCourseRepository.DeleteAllSavedcoursesAsync(userId);
            return result ? ResponseFactory.Ok() : ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }


}