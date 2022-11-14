using Azure.Identity;
using FXProject.Client.Pages.Admin.Services;
using FXProject.Server.ForexDbContext;
using FXProject.Server.Services.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Okta.AspNetCore;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddAzureKeyVault(
//        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
//        new ClientCertificateCredential(
//            builder.Configuration["AzureADDirectoryId"],
//            builder.Configuration["AzureADApplicationId"],
//            builder.Configuration["AzureADCertThumbprint"]));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("forexDbDev");
builder.Services.AddDbContext<FXDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// OIDC OKTA Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
})
.AddOktaWebApi(new OktaWebApiOptions()
{
    OktaDomain = builder.Configuration["Okta:OktaDomain"]
}).AddCookie();



// Add Services for the Controllers
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ILocationService, LocationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
