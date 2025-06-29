using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BikeShop.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            // MVC Controllers
            services.AddControllers();

            // API Versioning + explorer
            services
               .AddApiVersioning(options =>
               {
                   options.DefaultApiVersion = new ApiVersion(1, 0);
                   options.AssumeDefaultVersionWhenUnspecified = true;
                   options.ReportApiVersions = true;
                   options.ApiVersionReader = new UrlSegmentApiVersionReader();
               })
               .AddApiExplorer(opts =>
               {
                   opts.GroupNameFormat = "'v'VVV";
                   opts.SubstituteApiVersionInUrl = true;
               });

            // Swagger / OpenAPI, one doc per version
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opts =>
            {
                // we need a provider to enumerate versions
                var provider = services.BuildServiceProvider()
                                       .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    opts.SwaggerDoc(desc.GroupName, new OpenApiInfo
                    {
                        Title = "BikeShop API",
                        Version = desc.ApiVersion.ToString()
                    });
                }
            });

            return services;
        }
    }
}
