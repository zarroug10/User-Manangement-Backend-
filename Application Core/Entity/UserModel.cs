using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

public enum UserRole
{
    Admin,
    Client
}

public class User
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public UserRole Role { get; private set; }

    public User(string username, string email, string password, UserRole role)
    {
        SetUsername(username);
        SetEmail(email);
        SetPassword(password);
        SetRole(role);
    }

    public void SetId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be a positive number.");
        Id = id;
    }

    public void SetUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException("Username cannot be empty.");
        Username = username.Trim();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            throw new ArgumentException("Please enter a valid email.");
        Email = email;
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 6)
            throw new ArgumentException("Password is too weak.");
        Password = HashPassword(password);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public void SetRole(UserRole role)
    {
        Role = role;
    }
}
