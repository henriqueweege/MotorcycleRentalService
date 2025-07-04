using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MotorcycleRentalService.Api.Extensions;
using MotorcycleRentalService.Infrastructure;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddCommandHandlers();
builder.Services.AddQueryHandlers();
builder.Services.AddRepositories();
builder.Services.AddDomainService();
builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(busConfigurator =>
{

    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((ctx, config) =>
    {
        config.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        config.ConfigureEndpoints(ctx);
    });
}
);

builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo
    {

        Version = "1.0.0",
        Title = "Sistema de Aluguel de Motos",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

Log.Logger = new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information().CreateLogger();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options =>
options.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);
app.MapControllers();
app.Run();
