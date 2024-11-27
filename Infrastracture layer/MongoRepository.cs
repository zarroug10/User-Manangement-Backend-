using MongoDB.Bson;
using MongoDB.Driver;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public MongoUserRepository(MongoDbContext context)
    {
        _users = context.Users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

     public async Task<User>GetByUsernameAsync(string username)
    {
        return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> FilterUsersAsync(string username = null, string email = null, UserRole? role = null)
    {
        var filterBuilder = Builders<User>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrEmpty(username))
        {
            filter &= filterBuilder.Regex(u => u.Username, new BsonRegularExpression(username.Trim(), "i"));
        }

        if (!string.IsNullOrEmpty(email))
        {
filter &= filterBuilder.Regex(u => u.Email, new BsonRegularExpression(email.Trim(), "i"));
        }

        if (role.HasValue)
        {
            filter &= filterBuilder.Eq(u => u.Role, role.Value);
        }

        return await _users.Find(filter).ToListAsync();
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }

    public async Task SaveAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
    }

    public async Task DeleteAsync(int id)
    {
        await _users.DeleteOneAsync(u => u.Id == id);
    }
}
