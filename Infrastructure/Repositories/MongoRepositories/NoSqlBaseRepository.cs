using Infrastructure.Helper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories.MongoRepositories;

public class NoSqlBaseRepository<TEntity> where TEntity : class
{
    protected IMongoCollection<TEntity> _collection;

    public NoSqlBaseRepository(IOptions<MongoDbSettings> mongoDbSettings, string collectionName)
    {
        MongoClient client = new(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _collection = database.GetCollection<TEntity>(collectionName);
        
    }

}