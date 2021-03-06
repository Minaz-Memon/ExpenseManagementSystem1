using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagementSystem1.Models
{
    public class FriendsModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public string FriendsId { get; set; }

        //[ForeignKey("User")]
        //public string UserId { get; set; }
        //public User User { get; set; }
    }
}
