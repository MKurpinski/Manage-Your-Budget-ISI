using System;
using System.Collections.Generic;
using System.Text;
using ManageYourBudget.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection EnableDatabase(this IServiceCollection services, IConfiguration config)
        {
            return services.AddDbContext<BudgetContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
        }
    }
}
