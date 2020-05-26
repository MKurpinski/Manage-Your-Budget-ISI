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
    public class ExpenseRepository: BaseRepository, IExpenseRepository
    {
        public ExpenseRepository(BudgetContext context) : base(context)
        {
        }

        public async Task CreateExpense(Expense expense)
        {
            await Context.AddEntity(expense);
            Context.SaveChanges();
        }

        public async Task<Expense> GetExpense(int id)
        {
            return await Context.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void DeleteExpense(Expense expense)
        {
            Context.Expenses.Remove(expense);
            Context.SaveChanges();
        }

        public int UpdateExpense(Expense expense)
        {
            Context.UpdateEntity(expense);
            return Context.SaveChanges();
        }
    }
}
