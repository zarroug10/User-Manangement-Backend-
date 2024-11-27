using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    private IMongoCollection<BsonDocument> Counters => _database.GetCollection<BsonDocument>("Counters");

    public int GetNextUserId()
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", "UserId");
        var update = Builders<BsonDocument>.Update.Inc("Value", 1);
        var options = new FindOneAndUpdateOptions<BsonDocument>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };

        var result = Counters.FindOneAndUpdate(filter, update, options);
        return result["Value"].AsInt32;
    }
}
