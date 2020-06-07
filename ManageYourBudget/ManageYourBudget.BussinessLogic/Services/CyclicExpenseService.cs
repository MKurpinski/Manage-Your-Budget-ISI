using System;
using System.Collections.Generic;
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
    public class CyclicExpenseService: BaseService, ICyclicExpenseService
    {
        private readonly IWalletPermissionService _walletPermissionService;
        private readonly ICyclicExpenseRepository _cyclicExpenseRepository;

        public CyclicExpenseService(IMapper mapper,
            ICyclicExpenseRepository cyclicExpenseRepository, IWalletPermissionService walletPermissionService) : base(mapper)
        {
            _cyclicExpenseRepository = cyclicExpenseRepository;
            _walletPermissionService = walletPermissionService;
        }

        public async Task<Result<BaseExpenseDto>> CreateExpense(BaseModifyExpenseDto modifyExpenseDto, string userId)
        {
            if (!(modifyExpenseDto is AddCyclicExpenseDto expenseDto))
            {
                throw new ArgumentException();
            }

            var hasUserPermission = await _walletPermissionService.HasUserAccess(modifyExpenseDto.WalletId, userId);

            if (!hasUserPermission)
            {
                return Result<BaseExpenseDto>.Failure();
            }

            var mapped = Mapper.Map<CyclicExpense>(expenseDto);
            mapped.ModifiedById = userId;
            mapped.CreatedById = userId;
            if (mapped.StartingFrom.Date == DateTime.UtcNow.Date)
            {
                mapped.LastApplied = DateTime.UtcNow.Date;
            }

            await _cyclicExpenseRepository.CreateExpense(mapped);

            if (mapped.Id == default)
            {
                return Result<BaseExpenseDto>.Failure();
            }

            return Result<BaseExpenseDto>.Success(Mapper.Map<BaseExpenseDto>(mapped));
        }

        public async Task<Result> DeleteExpense(int expenseId, string userId)
        {
            var expense = await _cyclicExpenseRepository.GetExpense(expenseId);

            if (expense == null)
            {
                return Result.Failure();
            }

            var hasUserPermission = await _walletPermissionService.HasUserAccess(expense.WalletId, userId);

            if (!hasUserPermission)
            {
                return Result.Failure();
            }

            _cyclicExpenseRepository.DeleteExpense(expense);
            return Result.Success();
        }

        public async Task<Result> UpdateExpense(BaseModifyExpenseDto editExpenseDto, int expenseId, string userId)
        {
            if (!(editExpenseDto is AddCyclicExpenseDto expenseDto))
            {
                throw new ArgumentException();
            }

            var expense = await _cyclicExpenseRepository.GetExpense(expenseId);

            if (expense == null)
            {
                return Result.Failure();
            }

            var hasUserPermission = await _walletPermissionService.HasUserAccess(expense.WalletId, userId);

            if (!hasUserPermission)
            {
                return Result.Failure();
            }

            expense.Category = expenseDto.Category;
            expense.Name = expenseDto.Name;
            expense.Place = expenseDto.Place;
            expense.Price = expenseDto.Price;
            expense.PeriodType = expenseDto.PeriodType;
            expense.StartingFrom = expenseDto.StartingFrom;
            expense.ModifiedById = userId;

            var result = _cyclicExpenseRepository.UpdateExpense(expense);
            return result != default ? Result.Success() : Result.Failure();
        }

        public async Task<List<CyclicExpenseDto>> GetAllCyclicExpenses(string walletId, string userId)
        {
            var walletIdDeobfustaded = walletId.ToDeobfuscated();
            var hasUserPermission = await _walletPermissionService.HasUserAccess(walletId, userId);

            if (!hasUserPermission)
            {
                return new List<CyclicExpenseDto>();
            }

            var result = await _cyclicExpenseRepository.GetAll(walletIdDeobfustaded);

            var mapped = Mapper.Map<List<CyclicExpenseDto>>(result);
            return mapped;
        }
    }
}
