using ExpenseManagementSystem1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            IConfiguration configuration, ExpenseMSystemContext context)
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
            var records = _context.Friend.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return records;
        }

        //To get list of friends of a particular user
        public async Task<List<FriendsMapping>> GetFriendsByUserId(string UserId)
        {
            var query = await _context.Friend.Where(x => x.UserId == UserId).Include(x => x.User
            ).Include(x => x.Friend).ToListAsync();

            //var query = await (from p in _context.Friend
            //                   join u in _context.Users on p.UserId equals u.Id
            //                   where UserId == p.UserId
            //                   select new FriendsMapping()
            //                   {
            //                       UserId = p.UserId,
            //                       FriendId = p.FriendId,
            //                       User = _context.Users.First(x => x.Id == p.UserId),
            //                       Friend = _context.Users.First(x => x.Id == p.FriendId),
            //                   }).ToListAsync();
            return query;

        }


        //To Add Friends
        public async Task<FriendsMapping> AddFriendsAsync(string userId, string friendId)
        {
            FriendsMapping map = new FriendsMapping();
            map.UserId = userId;
            map.FriendId = friendId;
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

        //List of User
        public async Task<List<UserAC>> GetAllUSers() {
            var records = await  _context.Users.ToListAsync();
            var output = new List<UserAC>();
            records.ForEach(x =>
            {
                var userdata = new UserAC
                {
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                };

                output.Add(userdata);

            });

            return output;
        }


        //List of Transaction
        //public async Task<List<TransactionAC>> GetTransactionsAsync()
        //{
        //    //var records = await _context.TransactionMappings.ToListAsync();
        //    var query = await _context.TransactionMappings.Include("User").ToListAsync();

        //    var output = new List<TransactionAC>();
        //    query.ForEach(x =>
        //    {
        //        var userdata = new TransactionAC
        //        {
        //            TransactionId = x.TranscationId,
        //            Payee = x.Payee,
        //            Payer = x.Payer,
        //            Name = x.Name,
        //            Email = x.Email,
        //            PhoneNumber = x.PhoneNumber
        //        };

        //        output.Add(userdata);

        //    });

        //    return output;
        //}

        //Add Transcation
        public async Task<TransactionMapping> AddTransactionsAsync(string payer, string payee,int amount, DateTime date )
        {
            TransactionMapping map = new TransactionMapping();
            map.Payer = payer;
            map.Payee= payee;
            map.Amount = amount;
            map.Date = date;
            _context.TransactionMappings.Add(map);
            await _context.SaveChangesAsync();
            return map;

            //_context.TransactionMappings.Add(trans);
            //await _context.SaveChangesAsync();
            //return trans;
        }

        //Delete Transcation
        public async Task DeleteTransactionAsync(int id)
        {
            var dele = await _context.TransactionMappings.FirstAsync(x => x.TranscationId == id);

            _context.TransactionMappings.Remove(dele);
            await _context.SaveChangesAsync();
        }

        //to get the specific Record of Transaction
        public Task<TransactionMapping> GetTransactionById(int Id)
        {
            var records = _context.TransactionMappings.Where(x => x.TranscationId == Id).FirstOrDefaultAsync();
            return records;
        }
        //To get list of Transaction of a particular user
        public async Task<List<TransactionMapping>> GetTransactionsByUserId(string UserId)
        {
            var query = await (from f in _context.TransactionMappings where f.Payer == UserId select f).ToListAsync();
            return query;
        }

        //To Edit
        public async Task UpdateTransaction(int TransactionId,string payer, string payee, int amount, DateTime date)
        {
            var edit = await _context.TransactionMappings.FindAsync(TransactionId);
            TransactionMapping map = new TransactionMapping();
            map.Payer = payer;
            map.Payee = payee;
            map.Amount = amount;
            map.Date = date;
            _context.TransactionMappings.Update(map);
             if (edit != null)
            {
                edit.Payer = map.Payer;
                edit.Payee = map.Payee;
                edit.Amount = map.Amount;
                edit.Date = map.Date;

                await _context.SaveChangesAsync();
             }
        }
    }
}
