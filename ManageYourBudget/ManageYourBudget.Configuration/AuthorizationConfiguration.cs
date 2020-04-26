using System.Text;
using ManageYourBudget.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ManageYourBudget.Configuration
{
    public static class AuthorizationConfiguration
    {
        public static void EnableAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = CreateTokenValidationParameters(config);
                });
        }

        public static void UseConfiguredAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }


        private static TokenValidationParameters CreateTokenValidationParameters(IConfiguration configuration)
        {
            var jwtTokenOptions = new JwtTokenOptions();
            configuration.GetSection("JwtToken").Bind(jwtTokenOptions);

            return new TokenValidationParameters
            {
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Key)),
                ValidAudience = jwtTokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtTokenOptions.Issuer
            };
        }
    }
}
