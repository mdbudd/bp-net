namespace WebApi.Models.Users;

using WebApi.Entities;
public class UpdateRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public Role Role { get; set; }
    public string Password { get; set; } = null!;
}