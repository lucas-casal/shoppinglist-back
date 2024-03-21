using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppinglist_back.Models;

public class RelatedUsersRequest
{
    [Required]
    public string UserAId { get; set; }
    [ForeignKey("UserAId")]
    public virtual User UserA { get; set; }
    [Required]
    public string UserBId { get; set;}
    [ForeignKey("UserBId")]
    public virtual User UserB { get; set; }
    public bool? Approved { get; set; }
}
