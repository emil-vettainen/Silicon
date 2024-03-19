using Business.Dtos.Author;
using Business.Dtos.Course;
using Infrastructure.Entities.Api;
using Infrastructure.Repositories.Api;

namespace Business.Services.Api
{
    public class CourseService
    {
        private readonly AuthorService _authorService;
        private readonly CourseRepository _courseRepository;


        public CourseService(CourseRepository courseRepository, AuthorRepository authorRepository, AuthorService authorService)
        {
            _courseRepository = courseRepository;
            _authorService = authorService;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _courseRepository.DeleteAsync(x => x.Id == id);
                return result;
            }
            catch (Exception)
            {

           
            }
            return false;
        }


        public async Task<bool> Exists(CreateCourseDto dto)
        {
            try
            {
                var exists = await _courseRepository.ExistsAsync(x => x.Title == dto.Title);
                return exists;
            }
            catch (Exception)
            {

            }
            return false;
        }


        public async Task<CourseDto> UpdateCourseAsync(CreateCourseDto dto, int id)
        {
            try
            {
                var authorId = await _authorService.GetOrCreateAuthorAsync(new AuthorDto
                {
                    FullName = dto.FullName,
                    AuthorBiography = dto.AuthorBiography,
                    AuthorImageUrl = dto.AuthorImageUrl,
                    YoutubeUrl = dto.YoutubeUrl,
                    FollowersYoutube = dto.FollowersYoutube,
                    FacebookUrl = dto.FacebookUrl,
                    FollowersFacebook = dto.FollowersFacebook,
                });

                if (authorId == 0)
                {
                    return null!;
                }

                var newCourse = await _courseRepository.UpdateAsync(x => x.Id == id, new CourseEntity
                {
                    Id = id,
                    Title = dto.Title,
                    Ingress = dto.Ingress,
                    Description = dto.Description,
                    Price = dto.Price,
                    DiscountPrice = dto.DiscountPrice,
                    Hours = dto.Hours,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    ImageUrl = dto.ImageUrl,
                    IsBestSeller = dto.IsBestSeller,
                    Articles = dto.Articles,
                    Resources = dto.Resources,
                    LifetimeAccess = dto.LifetimeAccess,
                    Certificate = dto.Certificate,
                    AuthorId = authorId,
                });

                if (newCourse != null)
                {
                    return new CourseDto
                    {
                        Id = newCourse.Id,
                        Title = newCourse.Title,
                        Ingress = newCourse.Ingress,
                        Description = newCourse.Description,
                        Price = newCourse.Price,
                        DiscountPrice = newCourse.DiscountPrice,
                        Hours = newCourse.Hours,
                        LikesInNumbers = newCourse.LikesInNumbers,
                        LikesInProcent = newCourse.LikesInProcent,
                        ImageUrl = newCourse.ImageUrl,
                        IsBestSeller = newCourse.IsBestSeller,
                        Articles = newCourse.Articles,
                        Resources = newCourse.Resources,
                        LifetimeAccess = newCourse.LifetimeAccess,
                        Certificate = newCourse.Certificate,
                        FullName = newCourse.Author.FullName,
                        AuthorBiography = newCourse.Author.AuthorBiography,
                        AuthorImageUrl = newCourse.Author.AuthorImageUrl,
                        YoutubeUrl = newCourse.Author.YoutubeUrl,
                        FollowersYoutube = newCourse.Author.FollowersYoutube,
                        FacebookUrl = newCourse.Author.FacebookUrl,
                        FollowersFacebook = newCourse.Author.FollowersFacebook,

                    };
                }

            }
            catch (Exception)
            {

            }
            return null!;
        }


        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            try
            {
                var authorId = await _authorService.GetOrCreateAuthorAsync(new AuthorDto
                {
                    FullName = dto.FullName,
                    AuthorBiography = dto.AuthorBiography,
                    AuthorImageUrl = dto.AuthorImageUrl,
                    YoutubeUrl = dto.YoutubeUrl,
                    FollowersYoutube = dto.FollowersYoutube,
                    FacebookUrl = dto.FacebookUrl,
                    FollowersFacebook = dto.FollowersFacebook,
                });

                if (authorId == 0)
                {
                    return null!;
                }

                var createdCourse = await _courseRepository.CreateAsync(new CourseEntity
                {
                    Title = dto.Title,
                    Ingress = dto.Ingress,
                    Description = dto.Description,
                    Price = dto.Price,
                    DiscountPrice = dto.DiscountPrice,
                    Hours = dto.Hours,
                    LikesInNumbers = dto.LikesInNumbers,
                    LikesInProcent = dto.LikesInProcent,
                    ImageUrl = dto.ImageUrl,
                    IsBestSeller = dto.IsBestSeller,
                    Articles = dto.Articles,
                    Resources = dto.Resources,
                    LifetimeAccess = dto.LifetimeAccess,
                    Certificate = dto.Certificate,
                    AuthorId = authorId,
                });

                if (createdCourse != null)
                {
                    return new CourseDto
                    {
                        Id = createdCourse.Id,
                        Title = createdCourse.Title,
                        Ingress = createdCourse.Ingress,
                        Description = createdCourse.Description,
                        Price = createdCourse.Price,
                        DiscountPrice = createdCourse.DiscountPrice,
                        Hours = createdCourse.Hours,
                        LikesInNumbers = createdCourse.LikesInNumbers,
                        LikesInProcent = createdCourse.LikesInProcent,
                        ImageUrl = createdCourse.ImageUrl,
                        IsBestSeller = createdCourse.IsBestSeller,
                        Articles = createdCourse.Articles,
                        Resources = createdCourse.Resources,
                        LifetimeAccess = createdCourse.LifetimeAccess,
                        Certificate = createdCourse.Certificate,
                        FullName = createdCourse.Author.FullName,
                        AuthorBiography = createdCourse.Author.AuthorBiography,
                        AuthorImageUrl = createdCourse.Author.AuthorImageUrl,
                        YoutubeUrl = createdCourse.Author.YoutubeUrl,
                        FollowersYoutube = createdCourse.Author.FollowersYoutube,
                        FacebookUrl = createdCourse.Author.FacebookUrl,
                        FollowersFacebook = createdCourse.Author.FollowersFacebook,

                    };

                   
                }


            }
            catch (Exception)
            {

                
            }
            return null!;
        }

        public async Task<IEnumerable<CourseDto>> GetAll() 
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync();
                if (courses != null)
                {
                    var courseDto = courses.Select(course => new CourseDto
                    {
                        Id = course.Id,
                        Title = course.Title,
                        Ingress = course.Ingress,
                        Description = course.Description,
                        Price = course.Price,
                        DiscountPrice = course.DiscountPrice,
                        Hours = course.Hours,
                        LikesInNumbers = course.LikesInNumbers,
                        LikesInProcent = course.LikesInProcent,
                        ImageUrl = course.ImageUrl,
                        IsBestSeller = course.IsBestSeller,
                        Articles = course.Articles,
                        Resources = course.Resources,
                        LifetimeAccess = course.LifetimeAccess,
                        Certificate = course.Certificate,
                        FullName = course.Author.FullName,
                        AuthorBiography = course.Author.AuthorBiography,
                        AuthorImageUrl = course.Author.AuthorImageUrl,
                        YoutubeUrl = course.Author.YoutubeUrl,
                        FollowersYoutube = course.Author.FollowersYoutube,
                        FacebookUrl = course.Author.FacebookUrl,
                        FollowersFacebook = course.Author.FollowersFacebook,

                    });

                    return courseDto.ToList();
                }

                
            }
            catch (Exception)
            {

                throw;
            }
            return null!;

        }


        public async Task<CourseDto> GetOne(int id)
        {
            try
            {
                var course = await _courseRepository.GetOneAsync(x => x.Id == id);
                if (course != null)
                {
                    var courseDto = new CourseDto
                    {
                        Id = course.Id,
                        Title = course.Title,
                        Ingress = course.Ingress,
                        Description = course.Description,
                        Price = course.Price,
                        DiscountPrice = course.DiscountPrice,
                        Hours = course.Hours,
                        LikesInNumbers = course.LikesInNumbers,
                        LikesInProcent = course.LikesInProcent,
                        ImageUrl = course.ImageUrl,
                        IsBestSeller = course.IsBestSeller,
                        Articles = course.Articles,
                        Resources = course.Resources,
                        LifetimeAccess = course.LifetimeAccess,
                        Certificate = course.Certificate,
                        FullName = course.Author.FullName,
                        AuthorBiography = course.Author.AuthorBiography,
                        AuthorImageUrl = course.Author.AuthorImageUrl,
                        YoutubeUrl = course.Author.YoutubeUrl,
                        FollowersYoutube = course.Author.FollowersYoutube,
                        FacebookUrl = course.Author.FacebookUrl,
                        FollowersFacebook = course.Author.FollowersFacebook,

                    };

                    return courseDto;
                }

            }
            catch (Exception)
            {

            }

            return null!;
        }

    }
}
