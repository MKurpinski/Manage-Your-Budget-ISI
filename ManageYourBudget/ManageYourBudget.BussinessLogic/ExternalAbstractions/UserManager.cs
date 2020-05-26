using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private const int SEARCH_BATCH_SIZE = 10;
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

        public Task<IdentityResult> ChangePasswordAsync(User user, string password, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, password, newPassword);
        }

        public Task<IdentityResult> AddPasswordAsync(User user, string password)
        {
            return _userManager.AddPasswordAsync(user, password);
        }

        public Task<IdentityResult> UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public async Task<ICollection<User>> Search(string searchTerm, string userId)
        {
            searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : $"{searchTerm.ToLower()}%";
            var sqlSearchTerm = new SqlParameter("@searchTerm", searchTerm);
            if (searchTerm == null)
            {
                sqlSearchTerm.Value = DBNull.Value;
            }
            var sqlUserId = new SqlParameter("@currentUserId", userId);
            var sqlBatchSize = new SqlParameter("@batchSize", SEARCH_BATCH_SIZE);

            return await _userManager.Users
                .FromSql("exec budget_SearchUsers @searchTerm, @currentUserId, @batchSize", sqlSearchTerm, sqlUserId, sqlBatchSize)
                .ToListAsync();
        }

        public string HashPassword(User user, string password)
        {
            return _userManager.PasswordHasher.HashPassword(user, password);
        }
    }
}
