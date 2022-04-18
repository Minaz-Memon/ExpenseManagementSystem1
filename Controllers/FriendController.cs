using ExpenseManagementSystem1.Models;
using ExpenseManagementSystem1.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementSystem1.Controllers
{
    public class FriendController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public FriendController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("ListofFriends")]
        public async Task<IActionResult> GetFriendsAsync()
        {
            var result = await _accountRepository.GetFriendsAsync();
            return Ok(result);
        }

        [HttpPost("AddFriends")]
        public async Task<IActionResult> AddFriendsAsync(string UserId, string FriendId)
        {
            var result = await _accountRepository.AddFriendsAsync(UserId, FriendId);
            return Ok(result);
        }

        [HttpDelete("DeleteFriends")]
        public async Task<IActionResult> DeleteFriendsAsync(int Id)
        {
            await _accountRepository.DeleteFriendsAsync(Id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FriendsMapping>> GetFriendsById(int id)
        {
            try
            {
                var result = await _accountRepository.GetFriendsById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from the database");
            }
        }
        [HttpGet("EachUserFriend")]
        public async Task<ActionResult<FriendsMapping>> GetFriendsByUserId(string UserId)
        {
            try
            {
                var result = await _accountRepository.GetFriendsByUserId(UserId);
               

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from the database");
            }
        }

    }
}
