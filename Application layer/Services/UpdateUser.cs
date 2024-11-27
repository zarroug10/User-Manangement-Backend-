
public class UpdateUser
{
    private readonly IUserRepository _userRepository;

    public UpdateUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    
    public async Task UpdateUserAsync(int id, string username, string email, string password, UserRole role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) throw new Exception("User not found.");

        user.SetUsername(username);
        user.SetEmail(email);
        user.SetPassword(password);
        user.SetRole(role);

        await _userRepository.UpdateAsync(user);
    }
}
