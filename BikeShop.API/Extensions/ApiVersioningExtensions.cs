using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShop.Api.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersioningWithExplorer(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}
