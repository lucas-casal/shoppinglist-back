using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using Shoppinglist_back.Dtos.RelationProductsListsDto;
using System.Text.Json.Serialization;

namespace Shoppinglist_back.Dtos.ShoppingListDtos;

public class ReadShoppingListDto
{

    public int Id { get; set; }
    public string Title { get; set; }
    [JsonPropertyName("Members")]

    public ICollection<ReadRelationMembersListsThroughListDto> Members { get; set; }
    public ICollection<ReadRelationProductsListsThroughListIdDto> Products { get; set; }

}
