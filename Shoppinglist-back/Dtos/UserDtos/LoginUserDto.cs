using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.UserDtos;

public class LoginUserDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }


}
