using System.ComponentModel.DataAnnotations;

namespace NftApi.DTOs;

public class RegisterUserDto
{
    [Required]
    public string username { get; set;}
    [Required]
    public string password { get; set;}
    [Required]
    public string type { get; set;}
}