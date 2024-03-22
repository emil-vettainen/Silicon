using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Entities.MongoDb;

public class AuthorEntity
{
   
    public string FullName { get; set; } = null!;


    public string Biography { get; set; } = null!;

   
    public string? ProfileImageUrl { get; set; }


    public string YoutubeUrl { get; set; } = null!;

  
    public string Subscribers { get; set; } = null!;

    
    public string FacebookUrl { get; set; } = null!;

  
    public string Followers { get; set; } = null!;
}
