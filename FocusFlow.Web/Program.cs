using Blazored.LocalStorage;
using FocusFlow.Web;
using FocusFlow.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// API base URL - in production this would come from configuration
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001";

// Add Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add Authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Add HTTP client with auth header handler
builder.Services.AddScoped<AuthHeaderHandler>();
builder.Services.AddHttpClient("FocusFlowAPI", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler<AuthHeaderHandler>();

// Register HttpClient for services
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("FocusFlowAPI"));

// Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

await builder.Build().RunAsync();
