using Shoppinglist_back.Dtos.RelationMembersListsDtos;
using System.Text.Json.Serialization;

namespace Shoppinglist_back.Dtos.ShoppingListDtos;

public class ReadShoppingListDto
{

    public int Id { get; set; }
    public string Title { get; set; }
    [JsonPropertyName("Members")]

    public List<ReadRelationMembersListsThroughListDto> ReadRelationMLDtos { get; set; }

}
