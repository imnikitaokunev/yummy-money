using System;
using Application.Common.Helpers;
using Application.Common.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Api;

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
                if (context.HostingEnvironment.IsDevelopment())
                {
                    return;
                }

                var root = builder.Build();
                var vaultName = root["KeyVault:Vault"];

                Require.NotNullOrEmpty(vaultName, nameof(vaultName));

                builder.AddAzureKeyVault($"https://{vaultName}.vault.azure.net/",
                    new PrefixKeyVaultSecretManager());
            })
            .UseSerilog((context, configuration) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    configuration.Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.File("log.txt");
                    return;
                }

                var applicationName = context.Configuration["ApplicationName"];
                var blobStorageConnectionString = context.Configuration["AzureBlobStorage:ConnectionString"];

                Require.NotNullOrEmpty(applicationName, nameof(applicationName));
                Require.NotNullOrEmpty(blobStorageConnectionString, nameof(blobStorageConnectionString));

                configuration.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.AzureBlobStorage(blobStorageConnectionString,
                        outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss}|{Level} => UserId:{UserId} => RequestId:{RequestId} => RequestPath:{RequestPath} => {SourceContext}{NewLine}    {Message}{NewLine}{Exception}",
                        storageContainerName:
                        $"{applicationName}-{context.HostingEnvironment.EnvironmentName.ToLower().Replace('.', '-')}",
                        storageFileName:
                        $"{applicationName}-logs-{context.HostingEnvironment.EnvironmentName.ToLower().Replace('.', '-')}-{DateTime.UtcNow:yyyy-MM}.log")
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .ReadFrom.Configuration(context.Configuration);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseSerilog();
    }
}
