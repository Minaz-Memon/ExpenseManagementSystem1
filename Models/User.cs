using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ExpenseManagementSystem1.Models
{
    public partial class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;

        public ICollection<FriendsModel> Friends { get; set; }

        //public ICollection<TransactionMapping> TransactionMappings { get; set; }
    }
}
