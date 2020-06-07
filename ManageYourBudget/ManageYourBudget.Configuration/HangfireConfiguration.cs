using Hangfire;
using ManageYourBudget.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class HangfireConfiguration
    {
        public static void EnableSchedulableJobs(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(config.GetConnectionString("DefaultConnection")));
        }

        public static void UseSchedulableJobs(this IApplicationBuilder app)
        {
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate<CyclicExpensesJob>(job => job.Execute(), "30 1 * * ?");
            RecurringJob.AddOrUpdate<CurrencyRateJob>(job => job.Execute(), "0 */3 ? * *");
        }
    }
}
