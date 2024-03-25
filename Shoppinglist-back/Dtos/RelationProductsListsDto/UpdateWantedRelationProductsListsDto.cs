using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelationProductsListsDto;

public class UpdateWantedRelationProductsListsDto
{
    public int? ProductId { get; set; }
    public int? ShoppingListId { get; set; }
    [Required]
    public int QuantityWanted { get; set; }
}
