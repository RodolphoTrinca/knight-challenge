using Knight.Application.Interface;
using Knight.Application.Repository;
using Knight.Application.Services;
using Knight.Infra.Context;
using Knight.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataBaseSettings = builder.Configuration.GetSection("KnightStoreDatabase").Get<KnightsDatabaseSettings>();

if (dataBaseSettings == null)
{
    Console.WriteLine("Error to retrive databaseSettings from appsettings.json");
    Environment.Exit(1);
}

builder.Services.Configure<KnightsDatabaseSettings>(builder.Configuration.GetSection("KnightStoreDatabase"));
builder.Services.AddDbContext<KnightDbContext>(options =>
{
    options.UseMongoDB(dataBaseSettings.ConnectionString ?? "", dataBaseSettings.DatabaseName ?? "");
});

//Setup dependencies
builder.Services.AddScoped<IKnightService, KnightService>();
builder.Services.AddScoped<IKnightRepository, KnightRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
