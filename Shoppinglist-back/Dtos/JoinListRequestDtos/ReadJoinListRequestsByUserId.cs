namespace Shoppinglist_back.Dtos.JoinListRequestDtos;

public class ReadJoinListRequestsByUserId
{
    public int ShoppingListId { get; set; }
    public string Title { get; set; }
    public bool Invited { get; set; }
    public bool? Approved { get; set; }
}

