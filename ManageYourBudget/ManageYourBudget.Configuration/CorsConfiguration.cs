using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBudget.Configuration
{
    public static class CorsConfiguration
    {
        public static IServiceCollection EnableCors(this IServiceCollection services)
        {
            return services.AddCors();
        }

        public static void UseConfiguredCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        }
    }
}
