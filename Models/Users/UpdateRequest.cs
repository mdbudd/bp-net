namespace WebApi.Models.Users;

public class UpdateRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Password { get; set; } = null!;
}