namespace WebApi.Entities;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
    public Role Role { get; set; } = Role.User;

    [Required]
    [JsonIgnore]
    public string PasswordHash { get; set; } = null!;
}