using BikeShop.Domain.Interfaces;
using BikeShop.Infrastructure.Data;
using BikeShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // EF Core
            services.AddDbContext<BikeShopDbContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("BikeShop")));

            // Repositories
            services.AddScoped<IBikeRepository, EfBikeRepository>();

            return services;
        }
    }
}
