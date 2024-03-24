using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelationMembersListsDtos;

public class UpdateRelationMembersListsDto
{

    public string? UserId { get; set; }
    public int? ShoppingListId { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
}
