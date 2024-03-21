using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelatedUsersRequestDtos;

public class CreateRelatedUsersRequestDto
{
    [Required]
    public string UserAId { get; set; }
    [Required]
    public string UserBId { get; set; }
}
