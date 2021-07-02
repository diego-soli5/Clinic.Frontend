using Clinic.Data.Abstractions;
using Clinic.Data.Repositories;
using Clinic.CrossCutting.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clinic.CrossCutting.Abstractions;
using Clinic.CrossCutting.Helpers;
using Clinic.Domain.Abstractions;
using Clinic.Domain.Services;
using System;

namespace Clinic.Domain.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiOptions>(x =>
            {
                x.Domain = configuration.GetSection("AppOptions:ApiOptions:Domain").Value;
            });

            services.Configure<PaginationOptions>(x =>
            {
                x.DefaultPageSize = Convert.ToInt32(configuration.GetSection("AppOptions:PaginationOptions:DefaultPageSize").Value);
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
        }

        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddSingleton<IUriGenerator, UriGenerator>();
        }

        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
