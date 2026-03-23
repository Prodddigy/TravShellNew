using Microsoft.AspNetCore.Authentication.Cookies;
using TravShelNew.Components;
using MudBlazor.Services;
using TravShelNew.Components.Middleware;

using TravShell.Infrastructure;
using TravShell.App;

using Azure.Identity;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();


var keyVaultUri = builder.Configuration.AddAzureKeyVault(
    new Uri("https://googlemapsapi.vault.azure.net/"),
    new Azure.Identity.DefaultAzureCredential());

//var secretClient = new SecretClient(new Uri(keyVaultUri),new )


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //	options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        //	options.Cookie.MaxAge = TimeSpan.FromMinutes(60);
        options.AccessDeniedPath = "/access-denied";
        //options.LogoutPath = "/logout";
    });

builder.Services.AddAuthorization();
//builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMudServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TravShellAuthMiddleware>();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
