namespace Shoppinglist_back.Dtos.JoinListRequestDtos;

public class ReadJoinListRequestsByListId
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public bool Invited { get; set; }
    public bool? Approved { get; set; }
}
