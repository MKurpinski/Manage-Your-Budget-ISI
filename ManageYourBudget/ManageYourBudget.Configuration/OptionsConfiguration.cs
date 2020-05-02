using ManageYourBudget.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection EnableOptions(this IServiceCollection services, IConfiguration config)
        {
            return services.AddOptions()
                .Configure<JwtTokenOptions>(opts => config.GetSection(nameof(JwtTokenOptions)).Bind(opts))
                .Configure<GoogleOptions>(opts => config.GetSection(nameof(GoogleOptions)).Bind(opts))
                .Configure<FacebookOptions>(opts => config.GetSection(nameof(FacebookOptions)).Bind(opts))
                .Configure<PasswordResetOptions>(opts => config.GetSection(nameof(PasswordResetOptions)).Bind(opts))
                .Configure<ExchangeOptions>(opts => config.GetSection(nameof(ExchangeOptions)).Bind(opts))
                .Configure<ClientOptions>(opts => config.GetSection(nameof(ClientOptions)).Bind(opts));
        }
    }
}
