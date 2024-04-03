using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Course;

public class CreateCourseDto
{
    public string CourseTitle { get; set; } = null!;
    public string CourseDescription { get; set; } = null!;
    public string CourseIngress { get; set; } = null!;
    public bool IsBestseller { get; set; } = false;
    public string? CourseImageUrl { get; set; }
    public string CourseCategory { get; set; } = null!;
    public RatingDto Rating { get; set; } = null!;
    public PriceDto Price { get; set; } = null!;
    public IncludedDto Included { get; set; } = null!;
    public AuthorDto Author { get; set; } = null!;
    public List<HighlightsDto> Highlights { get; set; } = [];
    public List<ProgramDetailsDto> Content { get; set; } = [];
}

public class CourseCategoryDto
{
    public string Name { get; set; } = null!;
}


public class RatingDto
{
    public decimal InNumbers { get; set; }
    public decimal InProcent { get; set; }
}


public class PriceDto
{
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
}

public class IncludedDto
{
    public int HoursOfVideo { get; set; }
    public int Articles { get; set; }
    public int Resourses { get; set; }
    public bool LifetimeAccess { get; set; } = false;
    public bool Certificate { get; set; } = false;
}

public class AuthorDto
{
    public string FullName { get; set; } = null!;
    public string Biography { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
    public SocialMediaDto SocialMedia { get; set; } = null!;
}

public class SocialMediaDto
{
    public string YouTubeUrl { get; set; } = null!;
    public string Subscribers { get; set; } = null!;
    public string FacebookUrl { get; set; } = null!;
    public string Followers { get; set; } = null!;
}

public class HighlightsDto
{
    public string Highlight { get; set; } = null!;
}

public class ProgramDetailsDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}