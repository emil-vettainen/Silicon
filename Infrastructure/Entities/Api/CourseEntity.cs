namespace Infrastructure.Entities.Api;

public class CourseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string? DiscountPrice {  get; set; }
    public string Hours { get; set; } = null!;
    public string? LikesInNumbers { get; set; }
    public string? LikesInProcent { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsBestSeller { get; set; }
    public string? Articles { get; set; }
    public string? Resources { get; set; }
    public bool LifetimeAccess { get; set; }
    public bool Certificate {  get; set; }

    public int AuthorId { get; set; }
    public AuthorEntity Author { get; set; } = null!;

}
