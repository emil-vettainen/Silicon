using Business.Dtos.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Factories;
using Shared.Responses;
using System.Net.Http.Headers;
using System.Text;



namespace Business.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly UploadService _uploadService;


    public CourseService(HttpClient httpClient, IConfiguration configuration, UploadService uploadService)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _uploadService = uploadService;
    }


    public async Task<CourseResultDto> GetCoursesAsync(string? category, string? searchQuery, int pageNumber, int pageSize)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Courses"]}?category={category}&searchQuery={searchQuery}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseResultDto>(await response.Content.ReadAsStringAsync());
                if (result != null && result.Succeeded) 
                {
                    
                    return result;
                }
            }
        }
        catch (Exception)
        {
            //logger
        }
        return null!;
    }


    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Categories"]}?key={_configuration["Api:Key"]}");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync());
                return result;
            }
        }
        catch (Exception)
        {

            
        }
        return null!;
    }



    public async Task<ResponseResult> CreateCourseApiAsync(CreateCourseDto dto, IFormFile? courseImage, IFormFile? authorImage, string accessToken)
    {
        try
        {

            if (courseImage != null && courseImage.Length > 0)
            {
                dto.CourseImageUrl = await _uploadService.SaveFileAsync(courseImage, "courses-img");
            }
            if (authorImage != null && authorImage.Length > 0)
            {
                dto.Author.ProfileImageUrl = await _uploadService.SaveFileAsync(authorImage, "authors-img");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiUris:Courses"]}?key={_configuration["Api:Key"]}", content);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Created:
                    return ResponseFactory.Ok("Course has been created");

                case System.Net.HttpStatusCode.Conflict:
                    return ResponseFactory.Exists();

                case System.Net.HttpStatusCode.BadRequest:
                    return ResponseFactory.Error();

                case System.Net.HttpStatusCode.Unauthorized:
                    return ResponseFactory.Forbidden();

                default:
                    return ResponseFactory.Unavailable();
            }
        }
        catch (Exception)
        {
            //logger
            return ResponseFactory.Unavailable();
        }
    }



    public async Task<ResponseResult> UpdateCourseApiAsync(UpdateCourseDto dto, IFormFile? courseImage, IFormFile? authorImage, string accessToken)
    {
        try
        {

            if (courseImage != null && courseImage.Length > 0)
            {
                dto.CourseImageUrl = await _uploadService.SaveFileAsync(courseImage, "courses-img");
            }
            if (authorImage != null && authorImage.Length > 0)
            {
                dto.Author.ProfileImageUrl = await _uploadService.SaveFileAsync(authorImage, "authors-img");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_configuration["ApiUris:Courses"]}/{dto.Id}?key={_configuration["Api:Key"]}", content);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Created:
                    return ResponseFactory.Ok("Course has been created");

                case System.Net.HttpStatusCode.Conflict:
                    return ResponseFactory.Exists();

                case System.Net.HttpStatusCode.BadRequest:
                    return ResponseFactory.Error();

                case System.Net.HttpStatusCode.Unauthorized:
                    return ResponseFactory.Forbidden();

                default:
                    return ResponseFactory.Unavailable();
            }
        }
        catch (Exception)
        {
            //logger
            return ResponseFactory.Unavailable();
        }
    }






    public async Task<string> SaveFileAsync(IFormFile file)
    {
        try
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!);
            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        catch (Exception)
        {
            //Logger
            return string.Empty;
        }
    }



  


 
}