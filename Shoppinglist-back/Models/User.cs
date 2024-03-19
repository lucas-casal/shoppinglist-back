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
        public virtual ICollection<RelatedUsers> RelatedUsersAsUserA { get; set;  }
        public virtual ICollection<RelatedUsers> RelatedUsersAsUserB { get; set; }
        public virtual ICollection<RelatedUsersRequest> RelatedUsersRequestsAsUserA { get; set; }
        public virtual ICollection<RelatedUsersRequest> RelatedUsersRequestsAsUserB { get; set; }
        public virtual ICollection<JoinListRequest> JoinListRequests { get; set; }

    }
}
