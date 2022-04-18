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
        [HttpPost("AddTransactions")]
        public async Task<IActionResult> AddTransactionsAsync(string payer, string payee, int amount, DateTime date)
        {
            var result = await _accountRepository.AddTransactionsAsync(payer,payee,amount,date);
            return Ok();
        }
        [HttpDelete("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransactionAsync(int Id)
        {
            await _accountRepository.DeleteTransactionAsync(Id);
            return Ok();
        }
        [HttpGet("GetTransactionById")]

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
        [HttpGet("EachUserTransaction")]

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
        [HttpPut("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction(int TransactionId, string payer, string payee, int amount, DateTime date)
        {
            await _accountRepository.UpdateTransaction(TransactionId, payer,payee, amount, date);
            return Ok();
        }
    }
}
