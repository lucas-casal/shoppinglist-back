namespace Shoppinglist_back.Dtos.JoinListRequestDtos;

public class CreateJoinListRequestDto
{
    public int ShoppingListId { get; set; }
    public string UserId { get; set; }
    public bool Invited { get; set; }
}
