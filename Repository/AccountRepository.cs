using ExpenseManagementSystem1.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManagementSystem1.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;

        public AccountRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new User()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email

            };
            return await _userManager.CreateAsync(user, signUpModel.Password); 
        }
    }
}
