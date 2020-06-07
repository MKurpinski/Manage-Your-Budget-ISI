using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models.Expense;

namespace ManageYourBudget.Jobs
{
    public class CyclicExpensesJob: IBudgetJob
    {
        private readonly ICyclicExpenseRepository _cyclicExpenseRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private const int BATCH_SIZE = 50;

        public CyclicExpensesJob(ICyclicExpenseRepository cyclicExpenseRepository, IExpenseRepository expenseRepository, IMapper mapper)
        {
            _cyclicExpenseRepository = cyclicExpenseRepository;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task Execute()
        {
            var page = 0;
            var cyclicExpenses = await _cyclicExpenseRepository.GetAll(page++, BATCH_SIZE);

            while (cyclicExpenses.Any())
            {
                foreach (var cyclic in cyclicExpenses)
                {
                    if (cyclic.StartingFrom.Date >= DateTime.UtcNow.Date && GetNextAplyingDate(cyclic.PeriodType, cyclic.LastApplied).Date >= DateTime.UtcNow.Date)
                    {
                        await AddExpense(cyclic);
                        cyclic.LastApplied = DateTime.UtcNow.Date;
                        _cyclicExpenseRepository.UpdateExpense(cyclic);
                    }
                }
                cyclicExpenses = await _cyclicExpenseRepository.GetAll(page++, BATCH_SIZE);
            }
        }

        private DateTime GetNextAplyingDate(CyclicExpensePeriodType period, DateTime date)
        {
            return DateTimeCalculator.GetNextApplyingDate(date, period);
        }

        private async Task AddExpense(CyclicExpense expense)
        {
            await _expenseRepository.CreateExpense(_mapper.Map<Expense>(expense));
        }
    }
}
