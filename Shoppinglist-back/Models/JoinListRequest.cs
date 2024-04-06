using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppinglist_back.Models;

public class JoinListRequest
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
    public bool Invited { get; set; } //indica se o usuário foi convidado (true) ou se está pedindo para entrar (false)
    public bool? Approved {  get; set; } //indica se o convite foi aceito ou não
}
