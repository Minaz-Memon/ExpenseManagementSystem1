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


        //To get List of Friends
        public async Task<List<FriendsMapping>> GetFriendsAsync()
        {
            var records = await _context.Friend.ToListAsync();
            return records;
        }

        //to get the specific Record
        public Task<FriendsMapping> GetFriendsById(int Id)
        {
            // var query = (from f in _context.Friends where f.Id == Id select f).ToList();
            //IEnumerable<FriendsModel> search = from f in _context.Friends where f.Id == Id select f;

            var records = _context.Friend.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return records;
            // return (Task<FriendsModel>)search;
            // return query;
        }

        //To get list of friends of a particular user
        public async Task<List<FriendsMapping>> GetFriendsByUserId(string UserId)
        {
            var query = await (from f in _context.Friend where f.UserId == UserId select f).ToListAsync();
            return query;
        }

        //To Add Friends
        public async Task<FriendsMapping> AddFriendsAsync(string userId, string friendId)
       
        {

            FriendsMapping map = new FriendsMapping();
            map.UserId = userId;
            map.FriendId = friendId;
            //var obj = string UserId , string FriendsId;
            _context.Friend.Add(map);
            await _context.SaveChangesAsync();
            return map;
        }

        //to delete friends
        public async Task DeleteFriendsAsync(int id)
        {
            var dele = await _context.Friend.FirstAsync(x => x.Id == id );

            _context.Friend.Remove(dele);
            await _context.SaveChangesAsync();
        }



        //List of Transaction
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            var records = await _context.Transactions.ToListAsync();
            return records;
        }

        //Add Transcation
        public async Task<Transaction> AddTransactionsAsync(Transaction trans)
        {
            _context.Transactions.Add(trans);
            await _context.SaveChangesAsync();
            return trans;
        }


        //Edit Transcation


        //Delete Transcation

    }
}
