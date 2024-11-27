
public class DeleteUser
{
    private readonly IUserRepository _userRepository;

    public DeleteUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
        Console.WriteLine("User deleted successfully");
    }
}
