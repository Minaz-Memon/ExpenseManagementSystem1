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

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactionsAsync()
        {
            var result = await _accountRepository.GetTransactionsAsync();
            return Ok(result);
        }
        [HttpPost("addTransactions")]
        public async Task<IActionResult> AddTransactionsAsync(string payer, string payee, int amount, DateTime date)
        {
            var result = await _accountRepository.AddTransactionsAsync(payer,payee,amount,date);
            return Ok();
        }
        [HttpDelete("deleteTransaction")]
        public async Task<IActionResult> DeleteTransactionAsync(int Id)
        {
            await _accountRepository.DeleteTransactionAsync(Id);
            return Ok();
        }
        [HttpGet("getbyId")]

        public async Task<ActionResult<TransactionMapping>> GetTransactionById(int id)
        {
            try
            {
                var result = await _accountRepository.GetTransactionById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from the database");
            }
        }
        [HttpGet("eachUserTransaction")]

        public async Task<ActionResult<TransactionMapping>> GetTransactionsByUserId(string UserId)
        {
            try
            {
                var result = await _accountRepository.GetTransactionsByUserId(UserId);


                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from the database");
            }
        }
        [HttpPut("updateTransaction")]
        public async Task<IActionResult> UpdateTransaction(int TransactionId, string payer, string payee, int amount, DateTime date)
        {
            await _accountRepository.UpdateTransaction(TransactionId, payer,payee, amount, date);
            return Ok();
        }
    }
}
