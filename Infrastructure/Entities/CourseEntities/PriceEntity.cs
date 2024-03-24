using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Entities.MongoDb
{
    public class PriceEntity
    {
   
        public string OriginalPrice { get; set; } = null!;

    
        public string? DiscountPrice { get; set; }
    }
}
