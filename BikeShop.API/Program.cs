using BikeShop.Api.Extensions;           // your AddApiVersioningWithExplorer()
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;        // for IApiVersionDescriptionProvider

var builder = WebApplication.CreateBuilder(args);

// 1) MVC controllers
builder.Services.AddControllers();

// 2) API versioning + versioned ApiExplorer
//    (your extension should internally call AddApiVersioning and AddVersionedApiExplorer)
builder.Services.AddApiVersioningWithExplorer();

// 3) Standard OpenAPI “explorer”—needed for non?versioned bits (eg health checks)
builder.Services.AddEndpointsApiExplorer();

// 4) SwaggerGen: register one SwaggerDoc for each API version
builder.Services.AddSwaggerGen(options =>
{
    // We have to build a temporary provider so we can register SwaggerDocs now
    var provider = builder.Services
        .BuildServiceProvider()
        .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var desc in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(
            desc.GroupName,
            new OpenApiInfo
            {
                Title = "BikeShop API",
                Version = desc.ApiVersion.ToString()
            }
        );
    }
});

var app = builder.Build();

// only enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    // 5) Serve the generated JSON and the Swagger?UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint(
                $"/swagger/{desc.GroupName}/swagger.json",
                $"BikeShop {desc.GroupName.ToUpper()}");
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
