using TestDevPace.Business.DI;
var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultLocalConnection");

builder.Services.AddDataLayer(connectionString);
builder.Services.AddBusinessLayer();

builder.Services.AddMvc();

var app = builder.Build();

app.MapControllerRoute(name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}");

app.Run();
