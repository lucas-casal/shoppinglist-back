using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public User() : base() {}
        public virtual ICollection<RelationMembersLists> RelationMembersLists { get; set; }

    }
}
