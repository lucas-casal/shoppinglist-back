using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppinglist_back.Models;

public class RelationMembersLists
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    [Required]
    public int ShoppingListId { get; set; }
    public virtual ShoppingList ShoppingList { get; set; }

    [Required]
    public bool IsAdmin { get; set; }
}
