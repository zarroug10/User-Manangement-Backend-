
public class GetUser
{
    private readonly IUserRepository _userRepository;

    public GetUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAsync(string username, string email, string password, UserRole role)
    {
        var user = new User(username, email, password, role);
        await _userRepository.SaveAsync(user);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<IEnumerable<User>> FilterUsersAsync(string username = null, string email = null, UserRole? role = null)
    {
          return await _userRepository.FilterUsersAsync(username, email, role);
    } 
}
