using System.Security.Claims;

namespace ManageYourBudget.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return null;
            }
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
