using System;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models.Expense;
using ManageYourBudget.Dtos.Expense;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class ExpenseService: BaseService, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IWalletPermissionService _walletPermissionService;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IWalletPermissionService walletPermissionService) : base(mapper)
        {
            _expenseRepository = expenseRepository;
            _walletPermissionService = walletPermissionService;
        }

        public async Task<Result<BaseExpenseDto>> CreateExpense(BaseModifyExpenseDto modifyExpenseDto, string userId)
        {
            if (!(modifyExpenseDto is ModifyExpenseDto expenseDto))
            {
                throw new ArgumentException();
            }

            var hasUserPermission = await _walletPermissionService.HasUserAccess(modifyExpenseDto.WalletId, userId);

            if (!hasUserPermission)
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

            var hasUserPermission = await _walletPermissionService.HasUserAccess(expense.WalletId, userId);
            if (!hasUserPermission)
            {
                return Result<BaseExpenseDto>.Failure();
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

            var hasUserPermission = await _walletPermissionService.HasUserAccess(expense.WalletId, userId);

            if (!hasUserPermission)
            {
                return Result<BaseExpenseDto>.Failure();
            }

            expense.Date = expenseDto.Date;
            expense.Category = expenseDto.Category;
            expense.Type = expenseDto.Type;
            expense.Name = expenseDto.Name;
            expense.Place = expenseDto.Place;
            expense.Price = expenseDto.Price;
            expense.ModifiedById = userId;

            var result = _expenseRepository.UpdateExpense(expense);
            return result != default ? Result.Success() : Result.Failure();
        }
    }
}