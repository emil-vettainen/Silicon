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
    public List<HighlightsDto> Highlights { get; set; } = null!;
    public List<ProgramDetailsDto> Content { get; set; } = null!;
}