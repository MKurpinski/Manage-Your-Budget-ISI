using FluentEmail.Core;
using ManageYourBudget.EmailService.EventHandlers;
using ManageYourBudget.Shared;
using ManageYourBudget.Shared.Events;
using ManageYourBudget.Shared.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace ManageYourBudget.EmailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.EnableRabbitMq(Configuration);
            services.EnableSendingEmails(Configuration);
            services.AddTransient<IEventHandler<ISendEmailEvent>, SendMessageEventHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBusClient busClient, IEventHandler<ISendEmailEvent> handler)
        {

            busClient.SubscribeAsync<UnassignFromWalletMessage>(async message => {
                await handler.Handle(message);
            });
            busClient.SubscribeAsync<AssignToWalletMessage>(async message => {
                await handler.Handle(message);
            });
            busClient.SubscribeAsync<ResetPasswordMessage>(async message => {
                await handler.Handle(message);
            });
            busClient.SubscribeAsync<WalletArchivedMessage>(async message => {
                await handler.Handle(message);
            });
        }
    }
}
