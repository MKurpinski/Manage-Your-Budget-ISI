using System;
using AutoMapper;
using ManageYourBudget.Common.Enums;
using ManageYourBudget.DataAccess.Models;
using ManageYourBudget.Dtos.Auth;
using ManageYourBudget.Dtos.Auth.LoginDataProvider;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class MappingConfiguration
    {
        public static IServiceCollection EnableMapping(this IServiceCollection services)
        {
            return services.AddAutoMapper(CreateMappingExpression());
        }

        public static Action<IMapperConfigurationExpression> CreateMappingExpression()
        {
            return opt =>
            {
                opt.CreateMap<LocalRegisterUserDto, User>()
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                    .ForMember(dest => dest.RegisteredWith, opts => opts.MapFrom(_ => LoginProvider.Local));

                opt.CreateMap<ExternalRegisterUserDto, User>()
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email));

                opt.CreateMap<FacebookUserDto, ExternalRegisterUserDto>()
                    .ForMember(dest => dest.PictureSrc, opts => opts.MapFrom(src => src.Picture.PictureDataDto.Url));
            };
        }
    }
}
