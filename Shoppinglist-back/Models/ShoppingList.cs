using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Models
{
    public class ShoppingList
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<RelationMembersLists> RelationMembersLists { get; set; }
        public virtual ICollection<JoinListRequest> JoinListRequests { get; set; }
        public virtual ICollection<RelationProductsLists> RelationProductsLists { get; set; }


    }
}
