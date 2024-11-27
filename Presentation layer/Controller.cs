
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly GetUser _getuserService;
    private readonly AddUser _adduserService;
    private readonly UpdateUser _updateuserService;
    private readonly DeleteUser _deleteuserService;

    public UserController(GetUser getuserService,AddUser adduserService,UpdateUser updateuserService,DeleteUser deleteuserService)
    {
        _getuserService = getuserService;
        _adduserService = adduserService;
        _updateuserService = updateuserService;
        _deleteuserService = deleteuserService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        await _adduserService.CreateUserAsync(request.Username, request.Email, request.Password, request.Role);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _getuserService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }


        [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredUsers(
        [FromQuery] string? username, 
        [FromQuery] string? email, 
        [FromQuery] UserRole? role)
    {
        var users = await _getuserService.FilterUsersAsync(username, email, role);
        return Ok(users);
    }


        [HttpGet("")]
    public async Task<IActionResult> GetAllusers(int id)
    {
        var user = await _getuserService.GetAllUsersAsync();
        if (user == null) return NotFound();
        return Ok(user);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        await _updateuserService.UpdateUserAsync(id, request.Username, request.Email, request.Password, request.Role);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _deleteuserService.DeleteUserAsync(id);
        return NoContent();
    }
}
