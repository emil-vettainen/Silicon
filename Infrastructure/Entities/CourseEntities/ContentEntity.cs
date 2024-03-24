using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Entities.MongoDb;

public class ContentEntity
{
 
    public string Title { get; set; } = null!;


    public string Description { get; set; } = null!;
}
