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
    public int ListId { get; set; }
    [ForeignKey("ListId")]
    public virtual ShoppingList ShoppingList { get; set; }
    [Required]
    public bool Invited { get; set; }
    public bool? Approved {  get; set; }
}
