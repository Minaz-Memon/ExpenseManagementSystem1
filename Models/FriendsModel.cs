using System.ComponentModel.DataAnnotations;

namespace ExpenseManagementSystem1.Models
{
    public class FriendsModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
