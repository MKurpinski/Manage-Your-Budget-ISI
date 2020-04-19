using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ManageYourBudget.Common.Extensions
{
    public static class IdentityResultExtensions
    {
        public static Result ToResult(this IdentityResult identity)
        {
            if (identity.Succeeded)
            {
                return Result.Success();
            }

            var errorsDictionary = identity.Errors.Select(x => new KeyValuePair<string, string>(x.Code, x.Description))
                .ToDictionary(x => x.Key, x => x.Value);
            return Result.Failure(errorsDictionary);
        }
    }
}
