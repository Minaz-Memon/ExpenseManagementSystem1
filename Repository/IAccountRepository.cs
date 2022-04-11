using ExpenseManagementSystem1.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseManagementSystem1.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
    }
}
