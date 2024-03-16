using Shoppinglist_back.Models;
using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Dtos.ShoppingListDtos
{
    public class CreateShoppingListDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public ICollection<Member> Members { get; set; }
    }
}