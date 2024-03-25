using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelationProductsListsDto;

public class UpdateDescriptionRelationProductsListsDto
{
    public int? ProductId { get; set; }
    public int? ShoppingListId { get; set; }
    [Required]
    public string Description { get; set; }
}
