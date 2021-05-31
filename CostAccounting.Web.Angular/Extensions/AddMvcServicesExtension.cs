using System.Reflection;
using System.Text;
using CostAccounting.Services.Auth;
using CostAccounting.Services.Core;
using CostAccounting.Services.Membership;
using CostAccounting.Services.Security;
using CostAccounting.Services.Settings;
using CostAccounting.Web.Angular.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CostAccounting.Web.Angular.Extensions
{
    public static class AddMvcServicesExtension
    {
        public static void AddMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            var securitySettings = new SecuritySettings();
            configuration.Bind(nameof(securitySettings), securitySettings);
            services.AddSingleton(securitySettings);

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<ValidationFilter>();
                options.Filters.Add<ExceptionHandlingFilter>();
            }).AddFluentValidation(mvcConfiguration =>
                mvcConfiguration.RegisterValidatorsFromAssembly(Assembly.Load("CostAccounting.Services")));

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            // TODO: Add other services here.
        }
    }
}