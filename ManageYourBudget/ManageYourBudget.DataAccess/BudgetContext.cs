using System;
using System.Threading.Tasks;
using ManageYourBudget.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ManageYourBudget.DataAccess
{
    public class BudgetContext : IdentityDbContext<User>
    {
        public BudgetContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserWallet>().HasKey(x => new
            {
                x.UserId,
                x.WalletId
            });
        }

        public DbSet<UserWallet> UserWallets { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }

    public static class BudgetContextExtensiosn
    {
        public static Task<EntityEntry<T>> AddEntity<T>(this BudgetContext context, T itemToAdd) where T: class, IEntity
        {
            itemToAdd.Created = DateTime.UtcNow;
            itemToAdd.Modified = DateTime.UtcNow;
            return context.Set<T>().AddAsync(itemToAdd);
        }

        public static void UpdateEntity<T>(this BudgetContext context, T itemToUpdate) where T : class, IEntity
        {
            itemToUpdate.Modified = DateTime.UtcNow;
            context.Entry(itemToUpdate).State = EntityState.Modified;
        }
    }
}
