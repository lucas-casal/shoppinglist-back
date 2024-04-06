namespace Shoppinglist_back.Dtos.JoinListRequestDtos;

public class ReadJoinListRequestDto
{
    public int ShoppingListId { get; set; }
    public string ShoppingListName { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public bool Invited { get; set; }
    public bool? Approved { get; set; }
}
