using FXProject.Client;
using FXProject.Client.Pages.Admin.Services;
using FXProject.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("FXProject.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("FXProject.ServerAPI"));

builder.Services.AddScoped<IUserService_Web, UserService_Web>();
builder.Services.AddScoped<ILocationService_Web, LocationService_Web>();

//OIDC
builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = builder.Configuration.GetValue<string>("Okta:Authority");
    options.ProviderOptions.ClientId = builder.Configuration.GetValue<string>("Okta:ClientId");
    options.ProviderOptions.ResponseType = "code";
});//.AddAccountClaimsPrincipalFactory<OktaCustomClaims>();

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
