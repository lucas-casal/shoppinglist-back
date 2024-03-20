using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelatedUsersDtos;

public class ReadRelatedUsersDto
{
    public string UserAId { get; set; }
    public string NicknameA {  get; set; }
    public string UserBId { get; set; }
    public string NicknameB {  get; set; }

}
