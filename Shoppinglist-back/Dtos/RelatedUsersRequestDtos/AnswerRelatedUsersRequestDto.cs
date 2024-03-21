using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.RelatedUsersRequestDtos;

public class AnswerRelatedUsersRequestDto
{
    [Required]
    public bool Approved {  get; set; }
}
