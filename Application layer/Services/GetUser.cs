
public class GetUser
{
    private readonly IUserRepository _userRepository;

    public GetUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
