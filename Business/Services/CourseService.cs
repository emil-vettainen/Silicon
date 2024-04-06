using Business.Dtos.Course;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Responses;


namespace Business.Services;

public class CourseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CourseService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
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
            var response = await _httpClient.GetAsync($"{_configuration["ApiUris:Categories"]}");
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


    //public async Task<ResponseResult> SaveToFavoriteAsync(string id)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
}