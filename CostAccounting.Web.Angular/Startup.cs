using System;
using System.IO;
using CostAccounting.Services;
using CostAccounting.Web.Angular.Extensions;
using CostAccounting.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CostAccounting.Web.Angular
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbServices(_configuration);
            services.AddMvcServices(_configuration);
            MapsterConfiguration.Configure();

            //In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "dist";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                //spa.Options.SourcePath = Directory.GetCurrentDirectory();

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");

                    //spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
