using System;
using CostAccounting.Shared;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace CostAccounting.Web.Angular
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var environment = context.HostingEnvironment;

                    if (environment.IsDevelopment())
                    {
                        return;
                    }

                    var root = builder.Build();
                    var vaultName = root["KeyVault:Vault"];

                    Expect.NotNullOrEmpty(vaultName, nameof(vaultName));

                    builder.AddAzureKeyVault($"https://{vaultName}.vault.azure.net/", new PrefixKeyVaultSecretManager());
                })
                .UseSerilog((context, configuration) =>
                {
                    var elasticConfigurationUri = context.Configuration["ElasticConfiguration:Uri"];
                    var applicationName = context.Configuration["ApplicationName"];
                    var elasticConfigurationApiKey = context.Configuration["ElasticConfiguration:ApiKey"];

                    Expect.NotNullOrEmpty(elasticConfigurationUri, nameof(elasticConfigurationUri));
                    Expect.NotNullOrEmpty(applicationName, nameof(applicationName));
                    Expect.NotNullOrEmpty(elasticConfigurationApiKey, nameof(elasticConfigurationApiKey));

                    configuration.Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(
                            new ElasticsearchSinkOptions(new Uri(elasticConfigurationUri))
                            {
                                IndexFormat =
                                    $"{applicationName}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace('.', '-')}-{DateTime.UtcNow:yyyy-MM}",
                                AutoRegisterTemplate = true,
                                ModifyConnectionSettings = x =>
                                    x.ApiKeyAuthentication(
                                        new ApiKeyAuthenticationCredentials(elasticConfigurationApiKey)),
                                NumberOfShards = 2,
                                NumberOfReplicas = 1
                            })
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}