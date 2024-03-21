namespace Shoppinglist_back.Dtos.RelatedUsersRequestDtos;

public class ReadRelatedUsersRequestByUserIdDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public bool? Approved { get; set; }

}
