using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManageYourBudget.BussinessLogic.ExternalAbstractions
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<User> _userManager;

        public UserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.Created = DateTime.UtcNow;
            user.Modified = DateTime.UtcNow;
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            user.Created = DateTime.UtcNow;
            user.Modified = DateTime.UtcNow;
            return await _userManager.CreateAsync(user);
        }

        public Result VerifyPasswordHash(User user, string password)
        {
            if (user == null)
            {
                return Result.Failure();
            }
            var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? Result.Success() : Result.Failure();
        }

        public async Task<User> GetByAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userManager.Users.FirstOrDefaultAsync(predicate);
        }
    }
}
