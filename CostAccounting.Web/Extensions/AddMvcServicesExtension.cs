using System.Text;
using CostAccounting.Services.Implementation;
using CostAccounting.Services.Implementation.Core;
using CostAccounting.Services.Implementation.Membership;
using CostAccounting.Services.Interfaces;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CostAccounting.Web.Extensions
{
    public static class AddMvcServicesExtension
    {
        public static void AddMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: Must be refactored.

            var securitySettings = new SecuritySettings();
            configuration.Bind(nameof(securitySettings), securitySettings);
            services.AddSingleton(securitySettings);

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IAuthService, AuthService>();

            // TODO: Add other services here.
        }
    }
}