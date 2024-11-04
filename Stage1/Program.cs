using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Stage1;
using Stage1.Data;
using Stage1.Services;
using Stage1.Controllers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Stage1.Model;
using Stage1.Middleware;
using MassTransit;
using RabbitMQ.Client;
using Stage1.Consume;
using Stage1.Publisher;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddScoped<IOrderPublisher, OrderPublisher>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<DbTaskContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskDbConnection")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<Stage1.Consume.OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.Durable = true;
            e.AutoDelete = false;

            e.ConfigureConsumer<Stage1.Consume.OrderCreatedConsumer>(context);
        });
    });
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
