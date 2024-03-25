namespace Shoppinglist_back.Dtos.RelationProductsListsDto;

public class CreateRelationProductsListsDto
{
    public int ShoppingListId { get; set; }
    public int ProductId { get; set; }
    public string Description { get; set; }
    public int QuantityWanted { get; set; }
}
