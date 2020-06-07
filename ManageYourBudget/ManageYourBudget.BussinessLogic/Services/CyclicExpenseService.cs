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
        private readonly IWalletRepository _walletRepository;
        private readonly ICyclicExpenseRepository _cyclicExpenseRepository;

        public CyclicExpenseService(IMapper mapper,
            IWalletRepository walletRepository,
            ICyclicExpenseRepository cyclicExpenseRepository) : base(mapper)
        {
            _walletRepository = walletRepository;
            _cyclicExpenseRepository = cyclicExpenseRepository;
        }

        public async Task<Result<BaseExpenseDto>> CreateExpense(BaseModifyExpenseDto modifyExpenseDto, string userId)
        {
            if (!(modifyExpenseDto is AddCyclicExpenseDto expenseDto))
            {
                throw new ArgumentException();
            }
            var userWallet = await _walletRepository.Get(expenseDto.WalletId.ToDeobfuscated(), userId);
            if (userWallet == null)
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

            var userWallet = await _walletRepository.Get(expense.WalletId, userId);

            if (userWallet == null)
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

            var userWallet = await _walletRepository.Get(expenseDto.WalletId.ToDeobfuscated(), userId);

            if (userWallet == null)
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
            if (await _walletRepository.Get(walletIdDeobfustaded, userId) == null)
            {
                return new List<CyclicExpenseDto>();
            }

            var result = await _cyclicExpenseRepository.GetAll(walletIdDeobfustaded);

            var mapped = Mapper.Map<List<CyclicExpenseDto>>(result);
            return mapped;
        }
    }
}
