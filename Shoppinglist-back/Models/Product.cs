using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Models;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual ICollection<RelationProductsLists> RelationProductsLists { get; set; }

}
