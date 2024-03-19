using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelationMembersListsDtos;

public class CreateRelationMembersListsDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public int ShoppingListId { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
}
