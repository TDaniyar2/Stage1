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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<DbTaskContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskDbConnection")));


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
