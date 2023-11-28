using Microsoft.AspNetCore.Hosting;
using NatDMS;
using NatDMS.Models;
using Natural.Core.IServices;
using Naturals.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Natural.Core.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddScoped<IDistributorService, DistributorService>();
builder.Services.AddScoped<IRetailorService , RetailorService>();   
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.Configure<ApiDetails>(builder.Configuration.GetSection("ApiUrlDetails"));
builder.Services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();

builder.Services.AddAutoMapper(typeof(Program)); 


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Login}/{id?}");

app.Run();
