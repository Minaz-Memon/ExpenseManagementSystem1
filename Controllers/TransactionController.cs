using ExpenseManagementSystem1.Models;
using ExpenseManagementSystem1.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementSystem1.Controllers
{
    public class TransactionController : Controller
    {

        private readonly IAccountRepository _accountRepository;

        public TransactionController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("ListofTransaction")]
        public async Task<IActionResult> GetTransactionsAsync()
        {
            var result = await _accountRepository.GetTransactionsAsync();
            return Ok(result);
        }
        [HttpPost("AddTranscations")]
        public async Task<IActionResult> AddTransactionsAsync(Transaction trans)
        {
            var result = await _accountRepository.AddTransactionsAsync(trans);
            return Ok();
        }

    }
}
