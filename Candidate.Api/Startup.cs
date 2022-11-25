using System.Diagnostics.CodeAnalysis;
using Candidate.Api.Extensions;
using Candidate.Common.FileHelper;
using Candidate.Common.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Candidate.Api
{
    /// <summary>
    /// Start Up Class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Public Configuration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Dependencies
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);

        }

        /// <summary>
        /// Configure Pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureCustomMiddleware();
            app.Configure(env, Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            CreateFileIfNotExistAsync(env);
        }


        private static void CreateFileIfNotExistAsync(IWebHostEnvironment env)
        {
            var rootDirectory = env.ContentRootPath + "\\Files";
            FileHelper.CreateDirectory(rootDirectory);
            FileHelper.CreateFile($"{rootDirectory}\\Candidate.csv");

        }
    }
}