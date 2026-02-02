using SmartHome.Application;
using SmartHome.Infrastructure;
using SmartHomeApi.GraphQL.Mutations;
using SmartHomeApi.GraphQL.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDi();
builder.Services.AddInfrastructureDi(builder.Configuration);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<DummyQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<HomeMutations>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGraphQL();

app.Run();