namespace Infrastructure.Contexts;

public class MongoDbContext
{
    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set;} = null!;
    public string CollectionName { get; set; } = null!;
}
