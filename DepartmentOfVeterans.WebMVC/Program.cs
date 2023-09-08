using DepartmentOfVeterans.App.Services;
using DepartmentOfVeterans.WebMVC.Auth;
using DepartmentOfVeterans.WebMVC.Models.Contracts;
using DepartmentOfVeterans.WebMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Custome Authorization before register DI for AuthenticationService

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddTransient<CustomAuthorizationMessageHandler>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IClient, Client>(sp =>
{
    return new Client("https://localhost:7227", new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7227")
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.LoginPath = new PathString("/Account/login");
            options.AccessDeniedPath = new PathString("/Account/denied");
        });

builder.Services.AddHttpClient<Client>().AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

// Add services to controllers.
builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
