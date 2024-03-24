namespace Shoppinglist_back.Dtos.RelationMembersListsDtos;

public class ReadRelationMembersListsCompleteDto
{
    public int ShoppingListId { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public bool IsAdmin { get; set; }
}
