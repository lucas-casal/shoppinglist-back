using System.ComponentModel.DataAnnotations;

namespace Shoppinglist_back.Models
{
    public class ListAdminInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
