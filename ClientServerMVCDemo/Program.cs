using ClientServerMVCDemo.Data.Context;
using ClientServerMVCDemo.Data.UnitOfWork;
using ClientServerMVCDemo.Services.ClientServices;
using ClientServerMVCDemo.Services.ServerFunction;
using ClientServerMVCDemo.Services.ServerServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClientServerDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "ClientServerInMemoryDatabase");
});
builder.Services.AddScoped<IClientServerUnitOfWork, ClientServerUnitOfWork>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<IServerFunctionService, ServerFunctionService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();