using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ManageYourBudget.Common;
using ManageYourBudget.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace ManageYourBudget.BussinessLogic.ExternalAbstractions
{
    public interface IUserManager: IExternalAbstraction
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> CreateAsync(User user);
        Result VerifyPasswordHash(User user, string password);
        Task<User> GetByAsync(Expression<Func<User, bool>> predicate);
        Task<IdentityResult> ChangePasswordAsync(User user, string password, string newPassword);
        Task<IdentityResult> AddPasswordAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<ICollection<User>> Search(string searchTerm, int toSkipEntries, int batchSize, string userId);
        string HashPassword(User user, string password);
    }
}
