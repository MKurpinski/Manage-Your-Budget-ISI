using System;
using ManageYourBudget.Api.Attributes;
using ManageYourBudget.Configuration;
using ManageYourBudget.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace ManageYourBudget.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SetUpElasticSearch();
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.EnableSwagger();
            services.AddMvc(opts => opts.Filters.Add(new ValidateModelStateAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient();
            services.EnableCors();
            services.EnableOptions(Configuration);
            services.EnableDatabase(Configuration);
            services.EnableIdentity();
            services.EnableAuth(Configuration);
            services.EnableMapping();
            services.EnableRabbitMq(Configuration);
            services.EnableDistrubutedMemoryCache(Configuration);
            return DependencyInjectionConfiguration.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseConfiguredSwagger();
            app.UseConfiguredCors();
            app.UseConfiguredAuth();
            app.UseHttpsRedirection();
            loggerFactory.EnableSerilog();
            app.UseMvc();
        }

        private void SetUpElasticSearch()
        {
            var elasticUri = Configuration["ElasticConfiguration:Uri"];
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
        }
    }
}
