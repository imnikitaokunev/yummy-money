using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostAccounting.Web.Configures
{
    internal interface IConfigure
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
