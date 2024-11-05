namespace WebApi.Helpers;
using System.ComponentModel.DataAnnotations;

public class AppSettings
{
    [Required]
    public string Secret { get; set; } = null!;
}