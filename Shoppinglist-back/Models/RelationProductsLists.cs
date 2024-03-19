﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppinglist_back.Models;

public class RelationProductsLists
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
    [Required]
    public int ListId { get; set; }
    [ForeignKey("ListId")]
    public virtual ShoppingList ShoppingList { get; set; }
    public string Description { get; set; }
    [Required]
    public int QuantityWanted { get; set; }
    [DefaultValue(0)]
    public int QuantityBought { get; set; }

}
