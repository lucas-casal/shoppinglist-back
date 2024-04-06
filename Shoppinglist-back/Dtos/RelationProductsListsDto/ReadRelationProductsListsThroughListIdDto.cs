namespace Shoppinglist_back.Dtos.RelationProductsListsDto;

public class ReadRelationProductsListsThroughListIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int QuantityWanted { get; set; }
    public int QuantityBought { get; set; }
}
