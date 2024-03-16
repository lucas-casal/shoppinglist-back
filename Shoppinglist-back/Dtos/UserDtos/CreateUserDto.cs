using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.UserDtos;

public class CreateUserDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
