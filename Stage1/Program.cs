using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Stage1;
using Stage1.Data;
using Stage1.Services;
using Stage1.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DbTaskContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TaskDbConnection") ??
                      "Host=localhost;Port=5432;Database=Orders;Username=postgres;Password=1234"));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
