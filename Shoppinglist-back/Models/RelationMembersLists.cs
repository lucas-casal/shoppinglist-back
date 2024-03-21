using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppinglist_back.Models;

public class RelationMembersLists
{
    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [Required]
    public int ShoppingListId { get; set; }
    [ForeignKey("ShoppingListId")]
    public virtual ShoppingList ShoppingList { get; set; }

    [Required]
    public bool IsAdmin { get; set; }
}
