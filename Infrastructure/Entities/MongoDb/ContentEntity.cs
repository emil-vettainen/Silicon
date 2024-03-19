namespace Infrastructure.Entities.MongoDb;

public class ContentEntity
{
    public string Module { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
