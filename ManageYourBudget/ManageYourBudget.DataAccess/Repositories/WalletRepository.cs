using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageYourBudget.DataAccess.Repositories
{
    public class WalletRepository : BaseRepository, IWalletRepository
    {
        public WalletRepository(BudgetContext context) : base(context)
        {
        }

        public async Task<UserWallet> Add(UserWallet wallet)
        {
            await Context.AddEntity(wallet);
            Context.SaveChanges();
            return wallet;
        }

        public async Task<IList<UserWallet>> GetAll(string userId)
        {
            return await Context.UserWallets.Include(x => x.Wallet)
                .Where(x => x.UserId == userId && !x.Archived && !x.Wallet.Archived)
                .OrderByDescending(x => x.LastOpened ?? DateTime.MinValue)
                .ToListAsync();
        }

        public async Task<UserWallet> Get(int id, string userId, bool includeInActive = false)
        {
            return await Context.UserWallets.Include(x => x.Wallet)
                .FirstOrDefaultAsync(x => x.WalletId == id && x.UserId == userId && !x.Archived && !x.Wallet.Archived && (x.Role != WalletRole.InActive || includeInActive));
        }

        public async Task<int> Update<T>(T wallet) where T: Entity
        {
            Context.UpdateEntity(wallet);
            return await Context.SaveChangesAsync();
        }
    }
}
