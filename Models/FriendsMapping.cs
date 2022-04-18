using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagementSystem1.Models
{
    public class FriendsMapping
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User  { get; set; }

        public string FriendId { get; set; }

        [ForeignKey("FriendId")]
        public virtual User Friend { get; set; }

     

    }
}
