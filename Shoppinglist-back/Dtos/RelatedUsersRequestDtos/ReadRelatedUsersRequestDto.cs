namespace Shoppinglist_back.Dtos.RelatedUsersRequestDtos;

public class ReadRelatedUsersRequestDto
{
    public string UserA { get; set; }
    public string UserB { get; set; }
    public bool? IsApproved { get; set; }
}
