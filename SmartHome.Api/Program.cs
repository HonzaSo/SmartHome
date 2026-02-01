using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartHome.Infrastructure;
using SmartHome.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Database>(builder.Configuration.GetSection("Database"));
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    {
        var dbSettings = serviceProvider.GetRequiredService<IOptions<Database>>().Value;
        options.UseNpgsql(dbSettings.ConnectionString, x => x.MigrationsAssembly("SmartHome.Infrastructure"));
    }
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();