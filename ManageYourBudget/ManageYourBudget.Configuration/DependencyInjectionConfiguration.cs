﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using ManageYourBudget.BussinessLogic.ExternalAbstractions;
using ManageYourBudget.BussinessLogic.Interfaces;
using ManageYourBudget.BussinessLogic.Providers;
using ManageYourBudget.BussinessLogic.Providers.LoginData;
using ManageYourBudget.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static AutofacServiceProvider Configure(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterRepositories();
            builder.RegisterServices();
            builder.RegisterProviders();
            builder.RegisterExternalAbstractions();
            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }
        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public static void RegisterExternalAbstractions(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IExternalAbstraction).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public static void RegisterProviders(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IProvider).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public static void RegisterLoginDataProviders(this ContainerBuilder builder)
        {
            builder.RegisterType<FacebookLoginDataProvider>().As<ILoginDataProvider>();
            builder.RegisterType<GoogleLoginDataProvider>().As<ILoginDataProvider>();
            builder.RegisterType<LoginDataProviderFactory>().As<ILoginDataProviderFactory>();
        }

        private static void RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
