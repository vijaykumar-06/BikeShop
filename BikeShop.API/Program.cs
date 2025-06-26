using Microsoft.OpenApi.Models;
using BikeShop.Domain.Interfaces;
using BikeShop.Infrastructure.Data;
using BikeShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// EF Core + SQL Server
builder.Services.AddDbContext<BikeShopDbContext>(opt =>
  opt.UseSqlServer(builder.Configuration.GetConnectionString("BikeShop")));

// Repository
builder.Services.AddScoped<IBikeRepository, EfBikeRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(BikeShop.Application.Queries.GetAllBikesQuery).Assembly)
);
// Update the MediatR registration to use the correct overload that accepts an Action<Microsoft.Extensions.DependencyInjection.MediatRServiceConfiguration>.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(BikeShop.Application.Queries.GetAllBikesQuery).Assembly);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikeShop API v1");
    });
}

app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
