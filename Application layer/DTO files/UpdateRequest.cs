using System.ComponentModel.DataAnnotations;

public class UpdateUserRequest
{

    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }


    [EmailAddress]
    public string Email { get; set; }

     [MinLength(6)]
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
