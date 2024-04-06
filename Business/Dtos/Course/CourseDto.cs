namespace Business.Dtos.Course;

public class CourseDto
{
    public string Id { get; set; } = null!;
    public string CourseTitle { get; set; } = null!;
    public string CourseIngress { get; set; } = null!;
    public string CourseDescription { get; set; } = null!;
    public string? CourseImageUrl { get; set; }
    public bool IsBestseller { get; set; } = false;
    public string CourseCategory { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public RatingDto Rating { get; set; } = null!;
    public PriceDto Price { get; set; } = null!;
    public IncludedDto Included { get; set; } = null!;
    public AuthorDto Author { get; set; } = null!;
    public List<HighlightsDto> Highlights { get; set; } = null!;
    public List<ProgramDetailsDto> Content { get; set; } = null!;
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
    public SocialMediaDto? SocialMedia { get; set; }
}

public class SocialMediaDto
{
    public string? YouTubeUrl { get; set; }
    public string? Subscribers { get; set; }
    public string? FacebookUrl { get; set; }
    public string? Followers { get; set; }
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