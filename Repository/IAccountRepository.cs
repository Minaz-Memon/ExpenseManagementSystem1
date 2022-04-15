﻿using ExpenseManagementSystem1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementSystem1.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
        Task<IActionResult> Logout();

        Task<List<FriendsModel>> GetFriendsAsync();
    }
}
