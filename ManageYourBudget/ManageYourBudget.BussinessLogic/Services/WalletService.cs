using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.Common;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Interfaces;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Wallet;

namespace ManageYourBudget.BussinessLogic.Services
{
    public class WalletService : BaseService, IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IEmailService _emailService;

        public WalletService(IMapper mapper, IWalletRepository walletRepository, IEmailService emailService) : base(mapper)
        {
            _walletRepository = walletRepository;
            _emailService = emailService;
        }

        public async Task<Result<string>> CreateWallet(AddWalletDto addWalletDto, string userId)
        {
            var wallet = Mapper.Map<Wallet>(addWalletDto);
            wallet.CreatorId = userId;

            var userWallet = new UserWallet
            {
                Role = WalletRole.Creator,
                Wallet = wallet,
                UserId = userId
            };

            await _walletRepository.Add(userWallet);

            return userWallet.WalletId != default(int)
                ? Result<string>.Success(userWallet.WalletId.ToObfuscated())
                : Result<string>.Failure();
        }

        public async Task<List<BaseWalletDto>> GetWallets(string userId)
        {
            var result = await _walletRepository.GetAll(userId);
            var mappedResult = Mapper.Map<List<BaseWalletDto>>(result.ToList());
            return mappedResult;
        }

        public async Task<bool> HasAnyWallet(string userId)
        {
            return await _walletRepository.HasAny(userId);
        }

        public async Task<Result<ExtendedWalletDto>> GetWallet(string id, string userId)
        {
            var wallet = await _walletRepository.Get(id.ToDeobfuscated(), userId);
            if (wallet == null)
            {
                return Result<ExtendedWalletDto>.Failure();
            }
            wallet.LastOpened = DateTime.UtcNow; ;
            _walletRepository.Update(wallet);

            var mappedWallet = Mapper.Map<ExtendedWalletDto>(wallet);
            mappedWallet.Participants = mappedWallet.Participants.Where(x => x.User.Id != userId && x.Role != WalletRole.InActive.ToString()).ToList();
            return Result<ExtendedWalletDto>.Success(mappedWallet);
        }

        public async Task<Result> UpdateWallet(UpdateWalletDto updateWalletDto, string id, string userId)
        {
            var wallet = await _walletRepository.GetWithoutDependencies(id.ToDeobfuscated(), userId);
            if (wallet == null || !wallet.Role.HasAllPrivileges())
            {
                return Result<ExtendedWalletDto>.Failure();
            }

            wallet.Wallet.Name = updateWalletDto.Name;
            wallet.Wallet.Category = updateWalletDto.Category.ToEnumValue<WalletCategory>();

            var result = _walletRepository.Update(wallet.Wallet);
            return result == 0 ? Result.Failure() : Result.Success();
        }

        public async Task<Result> StarWallet(string id, string userId)
        {
            var userWallet = await _walletRepository.GetWithoutDependencies(id.ToDeobfuscated(), userId);
            if (userWallet == null)
            {
                return Result.Failure();
            }

            userWallet.Favorite = !userWallet.Favorite;
            var result = _walletRepository.Update(userWallet);
            return result == 0 ? Result.Failure() : Result.Success();
        }

        public async Task<Result> ArchiveWallet(string id, string userId)
        {
            var userWallet = await _walletRepository.GetWithoutDependencies(id.ToDeobfuscated(), userId);
            if (userWallet == null)
            {
                return Result.Failure();
            }

            var result = userWallet.Role.HasAllPrivileges()
                ?  ArchiveWholeWallet(userWallet, userId)
                :  ArchiveOnlyUserWallet(userWallet);

            return result;
        }

        private Result ArchiveOnlyUserWallet(UserWallet userWallet)
        {
            userWallet.Archived = true;
            var result =  _walletRepository.Update(userWallet);
            return result == 0 ? Result.Failure() : Result.Success();
        }

        private Result ArchiveWholeWallet(UserWallet userWallet, string userId)
        {
            var wallet = userWallet.Wallet;
            wallet.Archived = true;
            var result = _walletRepository.Update(wallet);
            _emailService.SendWalletArchivedEmail(userId, wallet);
            return result == 0 ? Result.Failure() : Result.Success();
        }
    }
}
