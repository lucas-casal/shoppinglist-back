using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppinglist_back.Models;

public class RelatedUsers
{
    public string? UserAId { get; set; }
    [ForeignKey("UserAId")]
    public virtual User UserA { get; set; }
    
    public string? UserBId { get; set; }
    [ForeignKey("UserBId")]
    public virtual User UserB { get; set; }
    public string? NicknameA {  get; set; }
    public string? NicknameB { get; set; }
}
