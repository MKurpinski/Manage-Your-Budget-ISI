using System.Linq;
using AutoMapper;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.Common.Extensions;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.DataAccess.Models.Expense;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;
using ManageYourBudget.Dtos.Expense;
using ManageYourBudget.Dtos.Profile;
using ManageYourBudget.Dtos.Wallet;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.MessagePatterns;
using Profile = ManageYourBudget.Common.Constants.Profile;

namespace ManageYourBudget.Configuration
{
    public static class MappingConfiguration
    {
        public static IServiceCollection EnableMapping(this IServiceCollection services)
        {
            return services.AddAutoMapper(opt =>
            {
                opt.CreateMap<LocalRegisterUserDto, User>()
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                    .ForMember(dest => dest.RegisteredWith, opts => opts.MapFrom(_ => LoginProvider.Local));

                opt.CreateMap<ExternalRegisterUserDto, User>()
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email));

                opt.CreateMap<FacebookUserDto, ExternalRegisterUserDto>()
                    .ForMember(dest => dest.PictureSrc, opts => opts.MapFrom(src => src.Picture.PictureDataDto.Url));

                opt.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.HasLocalAccount, opts => opts.MapFrom(src => src.HasLocalAccount()))
                    .AfterMap((src, dest) =>
                    {
                        if (dest.PictureSrc == null)
                        {
                            dest.PictureSrc = $"{Profile.GravatarUrl}{src.Email.ToMdHashed()}{Profile.GravatarSize}";
                        }
                    });

                opt.CreateMap<UserWallet, WalletParticipantDto>();

                opt.CreateMap<AddWalletDto, Wallet>()
                    .ForMember(dest => dest.Category,
                        opts => opts.MapFrom(src => src.Category.ToEnumValue<WalletCategory>()))
                    .ForMember(dest => dest.DefaultCurrency,
                        opts => opts.MapFrom(src => src.Category.ToEnumValue<SupportedCurrencies>()));

                opt.CreateMap<UserWallet, BaseWalletDto>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.WalletId.ToObfuscated()))
                    .ForMember(dest => dest.Category,
                        opts => opts.MapFrom(src => src.Wallet.Category.GetStringValue()))
                    .ForMember(dest => dest.Role,
                        opts => opts.MapFrom(src => src.Role.GetStringValue()))
                    .ForMember(dest => dest.DefaultCurrency,
                        opts => opts.MapFrom(src => src.Wallet.DefaultCurrency.GetStringValue()))
                    .ForMember(dest => dest.Name,
                        opts => opts.MapFrom(src => src.Wallet.Name));


                opt.CreateMap<UserWallet, ExtendedWalletDto>()
                    .IncludeBase<UserWallet, BaseWalletDto>()
                    .ForMember(dest => dest.Participants,
                        opts => opts.MapFrom(src =>
                            src.Wallet.UserWallets.Where(x => x.UserId != src.UserId)));

                opt.CreateMap<ModifyExpenseDto, Expense>()
                    .ForMember(dest => dest.WalletId,
                        opts => opts.MapFrom(src => src.WalletId.ToDeobfuscated()))
                    .ForMember(dest => dest.Category,
                        opts => opts.MapFrom(src => src.Category.ToEnumValue<ExpenseCategory>()));
            });

        }
    }
}
