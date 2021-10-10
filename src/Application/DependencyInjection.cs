using Application.Common.Mappings;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            MappingProfile.ApplyMappings();

            var config = TypeAdapterConfig.GlobalSettings;

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}