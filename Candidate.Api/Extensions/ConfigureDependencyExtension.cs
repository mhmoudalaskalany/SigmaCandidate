using System;
using System.Diagnostics.CodeAnalysis;
using Candidate.Application.Mapping;
using Candidate.Application.Services.Candidate;
using Candidate.Common.Abstraction.Repository;
using Candidate.Infrastructure.Context;
using Candidate.Infrastructure.Repository.CandidateRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Candidate.Api.Extensions
{
    /// <summary>
    /// Dependency Extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureDependencyExtension
    {
        private const string ConnectionStringName = "Default";
        /// <summary>
        /// Register Extensions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.RegisterDbContext(configuration);
            services.AddLocalizationServices();
            services.AddServices();
            services.RegisterRepositories();
            services.RegisterAutoMapper();
            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen(opt =>
            {
                opt.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        /// <summary>
        /// Add DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CandidateDbContext>(options =>
            {
                var inMemory = configuration["UseInMemoryDatabase"];
                if (inMemory == "True")
                {
                    options.UseInMemoryDatabase("CandidateDb");
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
                }
                
                
            });
            services.AddScoped<DbContext, CandidateDbContext>();
        }


        /// <summary>
        /// register auto-mapper
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingService));

        }

        /// <summary>
        /// Register localization
        /// </summary>
        /// <param name="services"></param>
        private static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddLocalization();
        }

        /// <summary>
        /// Register Application Services Dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICandidateService, CandidateService>();
        }



        /// <summary>
        /// Register Repositories
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<CandidateRepository>();
            services.AddScoped<CandidateCsvRepository>();
            services.AddScoped<Func<string, ICandidateRepository>>(serviceProvider => key =>
            {
                return key switch
                {
                    "Csv" => serviceProvider.GetService<CandidateCsvRepository>(),
                    "Database" => serviceProvider.GetService<CandidateRepository>(),
                    _ => serviceProvider.GetService<CandidateCsvRepository>()
                };
            });
        }
    }
}
