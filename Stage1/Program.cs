using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Stage1;


var builder = WebApplication.CreateBuilder();
var app = builder.Build();

using var db = new DbTaskContext();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;

    string expressionForGuid = @"^/api/orders/\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";

    if (path == "/api/orders" && request.Method == "GET")
    {
        await OrderService.GetAllOrders(response);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
    {
        var id = Guid.Parse(path.Value?.Split("/")[3]);
        await OrderService.GetOrder(id, response); 
    }
    else if (path == "/api/orders" && request.Method == "POST")
    {
        await OrderService.CreateOrder(response, request);
    }
    else if (path == "/api/orders" && request.Method == "PUT")
    {
        await OrderService.UpdateOrder(response, request);
    }
    else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
    {
        var id = Guid.Parse(path.Value?.Split("/")[3]);
        await OrderService.DeleteOrder(id, response); 
    }
});

app.Run();
