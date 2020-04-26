using ManageYourBudget.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class DistributedMemoryCacheConfiguration
    {
        public static IServiceCollection EnableDistrubutedMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDistributedRedisCache(options =>
            {
                var redisOptions = new RedisOptions();
                configuration.GetSection("RedisOptions").Bind(redisOptions);
                options.Configuration = redisOptions.ConnectionString;
                options.InstanceName = redisOptions.InstanceName;
            });
        }
    }
}
