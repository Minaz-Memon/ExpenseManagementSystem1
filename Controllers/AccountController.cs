using ExpenseManagementSystem1.Models;
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






       
    }
}
