﻿using ExpenseManagementSystem1.Models;
using ExpenseManagementSystem1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementSystem1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly SignInManager<User> signInManager;

        public AccountController(IAccountRepository accountRepository, SignInManager<User> signInManager)
        {
            _accountRepository = accountRepository;
            this.signInManager = signInManager;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signUpModel)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);
            if(result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);

        }
        [HttpPost("loginOut")]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            
            return Ok();
        }
        [HttpGet("ListofFriends")]
        public async Task<IActionResult> GetFriendsAsync()
        {
            var result = await _accountRepository.GetFriendsAsync();
            return Ok(result);
        }

        [HttpPost("AddFriends")]
        public async Task<IActionResult> AddFriendsAsync([FromBody] FriendsMapping friend)
        {
            var result = await _accountRepository.AddFriendsAsync(friend);
            return Ok();
        }
        [HttpDelete("DeleteFriends")]
        public async Task<IActionResult> DeleteFriendsAsync(int Id)
        {
            await _accountRepository.DeleteFriendsAsync(Id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FriendsMapping>> GetFriendsByUserId(int id)
        {
            try
            {
                var result = await _accountRepository.GetFriendsByUserId(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error retrieving data from the database");
            }
        }


        [HttpGet("ListofTranscation")]
        public async Task<IActionResult> GetTranscationsAsync()
        {
            var result = await _accountRepository.GetTranscationsAsync();
            return Ok(result);
        }
        [HttpPost("AddTranscations")]
        public async Task<IActionResult> AddTranscationsAsync(Transcation trans)
        {
            var result = await _accountRepository.AddTranscationsAsync(trans);
            return Ok();
        }

    }
}
