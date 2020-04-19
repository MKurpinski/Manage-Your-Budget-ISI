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
    }
}
