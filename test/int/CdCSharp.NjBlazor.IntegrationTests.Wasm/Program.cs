using CdCSharp.NjBlazor.Features.Localization.Abstractions;
using CdCSharp.NjBlazor.IntegrationTests.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddNjBlazor();

WebAssemblyHost host = builder.Build();

await host.SetDefaultCulture();

await host.RunAsync();

public static class LocalizationExtensions
{
    // Activate wasm localization on startup
    // <BlazorWebAssemblyLoadAllGlobalizationData>true</BlazorWebAssemblyLoadAllGlobalizationData>
    public static async Task SetDefaultCulture(this WebAssemblyHost host)
    {
        ILocalizationJsInterop localizationJs = host.Services.GetRequiredService<ILocalizationJsInterop>();
        CultureInfo? cookieCulture = await localizationJs.GetCultureAsync();
        CultureInfo culture;
        if (cookieCulture != null)
            culture = cookieCulture;
        else
            culture = new CultureInfo("en-US");

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
