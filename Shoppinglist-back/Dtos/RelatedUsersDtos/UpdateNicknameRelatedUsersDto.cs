using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelatedUsersDtos;

public class UpdateNicknameRelatedUsersDto
{
    public string? UserId { get; set; }
    public string? RelatedId { get; set; }
    [Required]
    public string Nickname { get; set; }
}
