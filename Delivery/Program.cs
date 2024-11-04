using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Delivery;
using Delivery.Data;
using Delivery.Services;
using Delivery.Controllers;
using Delivery.Model;
using MassTransit;
using Delivery.Consumer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DeliveryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddControllers();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<OrderCreatedConsumer>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>(); 

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
