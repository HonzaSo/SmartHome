using Serilog;
using SmartHome.Application;
using SmartHome.Infrastructure;
using SmartHomeApi;
using SmartHomeApi.GraphQL.Dtos.Homes;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.GraphQL.Mutations;
using SmartHomeApi.GraphQL.Queries;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: 
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<HomeQueries>()
        .AddTypeExtension<RoomQueries>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddTypeExtension<HomeMutations>()
        .AddTypeExtension<RoomMutations>()
    .AddUnionType<IGetRoomResult>(d => d
        .Name("GetRoomResult")
        .Type<ObjectType<RoomTypeResponse>>()
        .Type<ObjectType<GetErrorResult>>())
    .AddUnionType<IGetHomeResult>(d => d
        .Name("GetHomeResult")
        .Type<ObjectType<HomeTypeResponse>>()
        .Type<ObjectType<GetErrorResult>>());

builder.Services.AddApplicationDi();
builder.Services.AddInfrastructureDi(builder.Configuration);
builder.Services.AddApiDi();

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