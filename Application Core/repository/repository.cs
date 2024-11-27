
public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> FilterUsersAsync(string username = null, string email = null, UserRole? role = null);
    Task<IEnumerable<User>> GetAllAsync();
    Task SaveAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}
