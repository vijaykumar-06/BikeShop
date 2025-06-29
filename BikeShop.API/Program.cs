using Asp.Versioning.ApiExplorer;
using BikeShop.Api;
using BikeShop.Application;
using BikeShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//  Register by layer
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddApi();

var app = builder.Build();

//  Pipeline
if (app.Environment.IsDevelopment())
{
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
