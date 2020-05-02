using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.EmailService
{
    public static class EmailServiceConfiguration
    {
        public static void EnableSendingEmails(this IServiceCollection services, IConfiguration config)
        {
            var options = new EmailOptions();
            config.GetSection(nameof(EmailOptions)).Bind(options);
            services.AddFluentEmail(options.From, options.From)
                .AddRazorRenderer()
                .AddSendGridSender(options.ApiKey);
        }
    }
}
