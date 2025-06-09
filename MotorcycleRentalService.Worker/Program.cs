using MassTransit;
using Microsoft.EntityFrameworkCore;
using MotorcycleRentalService.Application.Consumers;
using MotorcycleRentalService.Domain.Events;
using MotorcycleRentalService.Infrastructure;
using MotorcycleRentalService.Infrastructure.Repository;
using MotorcycleRentalService.Infrastructure.Repository.Contracts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDefaultRepository<MotorcycleCreated>, DefaultRepository<MotorcycleCreated>>();

builder.Services.AddMassTransit(busConfigurator =>
{

    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<MotorcycleCreatedConsumer>();
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

Log.Logger = new LoggerConfiguration().WriteTo.Console().MinimumLevel.Information().CreateLogger();
            

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();
app.Run();
