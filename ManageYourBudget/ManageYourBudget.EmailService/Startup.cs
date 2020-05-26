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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.EnableRabbitMq(Configuration);
            services.EnableSendingEmails(Configuration);
            services.AddTransient<IEventHandler<ISendEmailEvent>, SendMessageEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBusClient busClient, IEventHandler<ISendEmailEvent> handler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

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
