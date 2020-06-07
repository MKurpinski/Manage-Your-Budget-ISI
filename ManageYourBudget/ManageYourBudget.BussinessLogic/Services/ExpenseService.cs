using System;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
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

        public async Task<Result<BaseExpenseDto>> CreateExpense(BaseModifyExpenseDto modifyExpenseDto, string userId)
        {
            if (!(modifyExpenseDto is ModifyExpenseDto expenseDto))
            {
                throw new ArgumentException();
            }

            var userWallet = await _walletRepository.Get(expenseDto.WalletId.ToDeobfuscated(), userId);
            if (userWallet == null)
            {
                return Result<BaseExpenseDto>.Failure();
            }

            var mapped = Mapper.Map<Expense>(expenseDto);
            mapped.ModifiedById = userId;
            mapped.CreatedById = userId;
            await _expenseRepository.CreateExpense(mapped);

            if (mapped.Id == default)
            {
                return Result<BaseExpenseDto>.Failure();
            }

            var addedExpense = await _expenseRepository.GetExpense(mapped.Id);
            var addedMapped = Mapper.Map<ExpenseDto>(addedExpense);
            return Result<BaseExpenseDto>.Success(addedMapped);
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

        public async Task<Result> UpdateExpense(BaseModifyExpenseDto editExpenseDto, int expenseId, string userId)
        {
            if (!(editExpenseDto is ModifyExpenseDto expenseDto))
            {
                throw new ArgumentException();
            }

            var expense = await _expenseRepository.GetExpense(expenseId);

            if (expense == null)
            {
                return Result.Failure();
            }

            var userWallet = await _walletRepository.Get(expenseDto.WalletId.ToDeobfuscated(), userId);

            if (userWallet == null)
            {
                return Result.Failure();
            }

            expense.Date = expenseDto.Date;
            expense.Category = expenseDto.Category;
            expense.Name = expenseDto.Name;
            expense.Place = expenseDto.Place;
            expense.Price = expenseDto.Price;
            expense.ModifiedById = userId;

            var result = _expenseRepository.UpdateExpense(expense);
            return result != default ? Result.Success() : Result.Failure();
        }
    }
}