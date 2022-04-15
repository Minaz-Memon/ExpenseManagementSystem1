using ExpenseManagementSystem1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseManagementSystem1.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ExpenseMSystemContext _context;

        public AccountRepository(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration, ExpenseMSystemContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new User()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
                Name = $"{signUpModel.FirstName} {signUpModel.LastName}",
                Password = signUpModel.Password

            };
            return await _userManager.CreateAsync(user, signUpModel.Password); 
        }
        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var testValue = (_configuration["JWT:Secret"]);
            var authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256)
                );
           return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Transcation>> GetTranscationsAsync()
        //{
          //  var records = await Transcation.ToListAsyn();
           // return records;
        //}
        //To get List of Friends
        public async Task<List<FriendsModel>> GetFriendsAsync()
        {
            var records = await _context.Friends.ToListAsync();
            return records;
        }
        //public async Task<List<FriendsModel>> GetFriendsByUserId(string Id)
        //{
          //  var records = await _context.Friends.Where(x=>x.UserId == Id);
            //return records;

        //}
        public async Task<FriendsModel> AddFriendsAsync(FriendsModel friend)
        {
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();
            return friend;
        }
        public async Task DeleteFriendsAsync(int Id)
        {
            var dele = new FriendsModel() { Id = Id };
            
            _context.Friends.Remove(dele);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Transcation>> GetTranscationsAsync()
        {
            var records = await _context.Transcations.ToListAsync();
            return records;
        }


    }
}
