using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Entities.MongoDb;

public class RatingEntity
{
    public string? InNumbers { get; set; }

    public string? InProcent {  get; set; }
}