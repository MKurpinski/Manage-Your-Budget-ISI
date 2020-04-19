using ManageYourBudget.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManageYourBudget.DataAccess
{
    public class BudgetContext : IdentityDbContext<User>
    {
        public BudgetContext(DbContextOptions options) : base(options)
        {
        }
    }
}
