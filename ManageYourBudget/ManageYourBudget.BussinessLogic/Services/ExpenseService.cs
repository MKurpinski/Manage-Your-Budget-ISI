using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models.Expense;
using ManageYourBudget.Dtos.Expense;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class ExpenseService: BaseService, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletRepository _walletRepository;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IWalletRepository walletRepository) : base(mapper)
        {
            _expenseRepository = expenseRepository;
            _walletRepository = walletRepository;
        }

        public async Task<Result> CreateExpense(ModifyExpenseDto modifyExpenseDto, string userId)
        {
            var userWallet = await _walletRepository.Get(modifyExpenseDto.WalletId.ToDeobfuscated(), userId);
            if (userWallet == null)
            {
                return Result.Failure();
            }

            var mapped = Mapper.Map<Expense>(modifyExpenseDto);
            mapped.ModifiedById = userId;
            mapped.CreatedById = userId;
            await _expenseRepository.CreateExpense(mapped);
            return mapped.Id != default ? Result.Success() : Result.Failure();
        }

        public async Task<Result> DeleteExpense(int expenseId, string userId)
        {
            var expense = await _expenseRepository.GetExpense(expenseId);

            if (expense == null)
            {
                return Result.Failure();
            }

            var userWallet = await _walletRepository.Get(expense.WalletId, userId);

            if (userWallet == null)
            {
                return Result.Failure();
            }

            _expenseRepository.DeleteExpense(expense);
            return Result.Success();
        }

        public async Task<Result> UpdateExpense(ModifyExpenseDto editExpenseDto, int expenseId, string userId)
        {
            var expense = await _expenseRepository.GetExpense(expenseId);

            if (expense == null)
            {
                return Result.Failure();
            }

            var userWallet = await _walletRepository.Get(editExpenseDto.WalletId.ToDeobfuscated(), userId);

            if (userWallet == null)
            {
                return Result.Failure();
            }

            expense.Date = editExpenseDto.Date;
            expense.Category = editExpenseDto.Category.ToEnumValue<ExpenseCategory>();
            expense.Name = editExpenseDto.Name;
            expense.Place = editExpenseDto.Place;
            expense.Price = editExpenseDto.Price;
            expense.ModifiedById = userId;

            var result = _expenseRepository.UpdateExpense(expense);
            return result != default ? Result.Success() : Result.Failure();
        }
    }
}