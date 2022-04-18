using ExpenseManagementSystem1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementSystem1.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
        Task<IActionResult> Logout();

        Task<List<FriendsMapping>> GetFriendsAsync();
        Task<FriendsMapping> AddFriendsAsync(string UserId, string FriendId);

        Task DeleteFriendsAsync(int Id);
        Task<FriendsMapping> GetFriendsById(int Id);
        Task<List<FriendsMapping>> GetFriendsByUserId(string UserId);

        Task<List<TransactionMapping>> GetTransactionsAsync();
        Task<TransactionMapping> AddTransactionsAsync(string payer, string payee, int amount, DateTime date);
        Task DeleteTransactionAsync(int id);
        Task<TransactionMapping> GetTransactionById(int Id);
        Task<List<TransactionMapping>> GetTransactionsByUserId(string UserId);
        Task UpdateTransaction(int TransactionId, string payer, string payee, int amount, DateTime date);
    }
}
