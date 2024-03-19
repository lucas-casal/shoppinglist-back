using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.ShoppingListDtos;

public class UpdateShoppingListDto
{
    [Required]
    public string Title { get; set; }
}
