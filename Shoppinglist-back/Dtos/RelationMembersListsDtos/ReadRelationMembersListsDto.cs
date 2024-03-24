namespace Shoppinglist_back.Dtos.RelationMembersListsDtos;

public class ReadRelationMembersListsDto
{
    public string User { get; set; }
    public string ShoppingList { get; set; }
    public bool IsAdmin { get; set; }
}
