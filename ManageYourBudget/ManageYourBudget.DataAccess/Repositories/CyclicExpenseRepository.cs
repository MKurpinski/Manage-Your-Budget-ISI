using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models.Expense;
using Microsoft.EntityFrameworkCore;

namespace ManageYourBudget.DataAccess.Repositories
{
    public class CyclicExpenseRepository: BaseRepository, ICyclicExpenseRepository
    {
        public CyclicExpenseRepository(BudgetContext context) : base(context)
        {
        }

        public async Task CreateExpense(CyclicExpense expense)
        {
            await Context.AddEntity(expense);
            Context.SaveChanges();
        }

        public async Task<CyclicExpense> GetExpense(int id)
        {
            return await Context.CyclicExpenses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CyclicExpense>> GetAll(int walletId)
        {
            return await Context.CyclicExpenses.Where(x => x.WalletId == walletId).ToListAsync();
        }

        public async Task<List<CyclicExpense>> GetAll(int page, int batchSize)
        {
            return await Context.CyclicExpenses.Skip(page * batchSize).Take(batchSize).ToListAsync();
        }

        public void DeleteExpense(CyclicExpense expense)
        {
            Context.CyclicExpenses.Remove(expense);
            Context.SaveChanges();
        }

        public int UpdateExpense(CyclicExpense expense)
        {
            Context.UpdateEntity(expense);
            return Context.SaveChanges();
        }
    }
}
