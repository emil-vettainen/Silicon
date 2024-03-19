using Amazon.Runtime.CredentialManagement;
using Infrastructure.Contexts;
using Infrastructure.Entities.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories.MongoDb;

public class MongoRepository
{
    private readonly IMongoCollection<CourseEntity> _courseCollection;

    public MongoRepository(IOptions<MongoDbContext> mongoDbContext)
    {
        MongoClient client = new MongoClient(mongoDbContext.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbContext.Value.DatabaseName);
        _courseCollection = database.GetCollection<CourseEntity>(mongoDbContext.Value.CollectionName);
    }


    public async Task <IEnumerable<CourseEntity>> GetAllAsync()
    {
        return await _courseCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<CourseEntity> GetOneAsync(string id)
    {
        var filter = Builders<CourseEntity>.Filter.Eq(x => x.CourseId, id);
        return await _courseCollection.Find(filter).FirstOrDefaultAsync();
    }


    public async Task<CourseEntity> CreateAsync(CourseEntity entity)
    {
       await _courseCollection.InsertOneAsync(entity);
       return entity;
        
    }
}
