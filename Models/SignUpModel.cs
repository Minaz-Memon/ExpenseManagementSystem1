using System.ComponentModel.DataAnnotations;

namespace ExpenseManagementSystem1.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare ("ConfirmPassword")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
