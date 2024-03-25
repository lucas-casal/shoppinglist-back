namespace Shoppinglist_back.Dtos.RelationProductsListsDto;

public class ReadRelationProductsListsDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ShoppingListId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int QuantityWanted { get; set; }
    public int QuantityBought { get; set; }

}
