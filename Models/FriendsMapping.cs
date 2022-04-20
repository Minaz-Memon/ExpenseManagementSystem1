using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagementSystem1.Models
{
    public class FriendsMapping
    {
        [Key]
        public int Id { get; set; }

        

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual User User  { get; set; }

        [ForeignKey("FriendId")]
        public string FriendId { get; set; }
        public virtual User Friend { get; set; }
        //public string Name { get; internal set; }
        //public string Email { get; internal set; }
    }
}
