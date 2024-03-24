using Amazon.Runtime.CredentialManagement;
using Infrastructure.Contexts;
using Infrastructure.Entities.MongoDb;
using Infrastructure.Helper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories.MongoRepositories;

public class MongoRepository
{
    private readonly IMongoCollection<CourseEntity> _courseCollection;

    public MongoRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _courseCollection = database.GetCollection<CourseEntity>(mongoDbSettings.Value.CollectionName);
    }
    

    public async Task<IEnumerable<CourseEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _courseCollection.Find(FilterDefinition<CourseEntity>.Empty).ToListAsync();
            return entities;
        }
        catch (Exception)
        {
            // logger
        }
        return null!;
    }

    public async Task<CourseEntity> GetOneAsync(string id)
    {
        try
        {
            var entity = await _courseCollection.Find(x => x.CourseId == id).FirstOrDefaultAsync();
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception)
        {
            //logger
        }
        return null!;
    }


    public async Task<CourseEntity> CreateAsync(CourseEntity entity)
    {
        try
        {
            await _courseCollection.InsertOneAsync(entity);
            return entity;
        }
        catch (Exception)
        {
           //logger
        }
        return null!;
    }

    public async Task<CourseEntity> UpdateAsync(CourseEntity entity)
    {
        try
        {

         

            //await _courseCollection.ReplaceOneAsync(x => x.CourseId == entity.CourseId, entity);
            //return entity;

           
                var result = await _courseCollection.ReplaceOneAsync(x => x.CourseId == entity.CourseId, entity);
                if (result.ModifiedCount > 0)
                {
                    return entity;
                }

            


           

        }
        catch (Exception)
        {

            //logger
        }
        return null!;
    }


    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var filter = Builders<CourseEntity>.Filter.Eq(x => x.CourseId, id);
            var result = await _courseCollection.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
            {
                return true;
            }
        }
        catch (Exception)
        {

            //logger
        }
        return false;
    }


}