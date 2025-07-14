using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorApp;
using MyBlazorApp.Localization;
using MudBlazor.Services;
using Toolbelt.Blazor.I18nText;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Локализация
builder.Services.AddI18nText<MyResources>();

var host = builder.Build();

// ⚠️ Критически важно: установка культуры до RunAsync
var js = host.Services.GetRequiredService<IJSRuntime>();
var savedCulture = await js.InvokeAsync<string>("localStorage.getItem", "culture");

if (!string.IsNullOrWhiteSpace(savedCulture))
{
  var culture = new CultureInfo(savedCulture);
  CultureInfo.DefaultThreadCurrentCulture = culture;
  CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await host.RunAsync();
