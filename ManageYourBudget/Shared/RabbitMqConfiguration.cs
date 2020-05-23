using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;

namespace ManageYourBudget.Shared
{
    public static class RabbitMqConfiguration
    {
    
        public static void EnableRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(RawRabbitConfiguration)).Get<RawRabbitConfiguration>();
            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = config
            });
        }
    }
}