using Knight.Application.Interface;
using Knight.Application.Repository;
using Knight.Application.Services;
using Knight.Infra.Context;
using Knight.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(options => {     	
			options.CacheProfiles.Add("Default30seconds", new CacheProfile { Duration = 30 });})
            .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHttpLogging(logging => {
     logging.LoggingFields = HttpLoggingFields.All;
     logging.RequestBodyLogLimit = 4096;
     logging.ResponseBodyLogLimit = 4096;
 });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Knight Challange API",
        Version = "v1",
        Description = "Knight control API",
    });

    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

//Setup dependencies
builder.Services.AddScoped<IKnightService, KnightService>();
builder.Services.AddScoped<IKnightRepository, KnightRepository>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "api/docs/{documentname}/swagger.json";

        options.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            //Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
            swagger.Servers.Clear();

        });
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("docs/v1/swagger.json", "Knight Challange API v1");
        c.RoutePrefix = "api";
    });
}

app.UseRouting();
app.UseHttpLogging();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
