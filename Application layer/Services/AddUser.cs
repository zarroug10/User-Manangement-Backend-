public class AddUser
{
    private readonly IUserRepository _userRepository;
    private readonly MongoDbContext _dbContext;

    public AddUser(IUserRepository userRepository, MongoDbContext dbContext)
    {
        _userRepository = userRepository;
        _dbContext = dbContext;
    }

    public async Task CreateUserAsync(string username, string email, string password, UserRole role)
    {
        int newUserId = _dbContext.GetNextUserId();

        var user = new User(username, email, password, role);
        user.SetId(newUserId); 

        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        var existingUsername = await _userRepository.GetByUsernameAsync(user.Username);
        if (existingUser != null){
            throw new Exception("User already exists.");
        }
        else if (existingUsername != null){
             throw new Exception("Username already already used.");
        }

        await _userRepository.SaveAsync(user);
        Console.WriteLine("User Added Successfully");
    }
}
