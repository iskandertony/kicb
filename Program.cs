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
builder.Services.AddI18nText<MyResources>();

var host = builder.Build();

// Важно: здесь JSRuntime доступен только после Build()
var js = host.Services.GetRequiredService<IJSRuntime>();

// Читаем язык из localStorage
var lang = await js.InvokeAsync<string>("localStorage.getItem", "culture") ?? "ru";

// Применяем культуру
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(lang);
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(lang);

// Показываем в консоли
await js.InvokeVoidAsync("console.log", $"[Startup] CurrentCulture = {CultureInfo.CurrentCulture}");

await host.RunAsync();
