using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities.MongoDb;

public class CourseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CourseId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Duration { get; set; }
    public bool IsBestSeller { get; set; } = false;
    public RatingEntity? Rating { get; set; }
    public PriceEntity Price { get; set; } = null!;
    public IncludedEntity Included { get; set; } = null!;
    public AuthorEntity Author { get; set; } = null!;
    public List<ContentEntity> CourseContents { get; set; } = [];
}