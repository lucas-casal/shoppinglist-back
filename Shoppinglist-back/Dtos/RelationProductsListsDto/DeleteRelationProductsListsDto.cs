using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelationProductsListsDto;

public class DeleteRelationProductsListsDto
{
    [Required]
    public int ShoppingListId { get; set; }
    [Required]
    public int ProductId { get; set; }
}
