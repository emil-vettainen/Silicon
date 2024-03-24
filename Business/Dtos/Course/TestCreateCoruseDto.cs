namespace Business.Dtos.Course
{
    public class TestCreateCoruseDto
    {
        public string CourseId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public bool IsBestSeller { get; set; } = false;
        public RatingDto? Rating { get; set; }
        public PriceDto Price { get; set; } = null!;
        public IncludedDto Included { get; set; } = null!;
        public AuthorDto Author { get; set; } = null!;
        public List<ContentDto> Content { get; set; } = [];  
    }
}
