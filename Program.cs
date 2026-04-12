using ElectronNET.API;
using ElectronWebsiteWrapper;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("ElectronWebsiteWrapper_");
builder.Services.AddRazorPages();
builder.Services.AddElectron();
builder.Services.AddOptions<AppSettings>()
    .Bind(builder.Configuration)
    .Validate(
        settings => !string.IsNullOrWhiteSpace(settings.Url) && Uri.TryCreate(settings.Url, UriKind.Absolute, out _),
        "The 'Url' configuration value must be a non-empty, valid absolute URI (e.g. https://www.example.com).")
    .ValidateOnStart();

WebApplication? app = null;
builder.UseElectron(args,
    async () =>
    {
        var settings = app!.Services.GetRequiredService<IOptions<AppSettings>>().Value;
        var browserWindow = await Electron.WindowManager.CreateWindowAsync(settings.Url);
        browserWindow.OnPageTitleUpdated += _ => browserWindow.SetTitle("ElectronWebsiteWrapper");
        if (OperatingSystem.IsWindows())
        {
            browserWindow.SetMenuBarVisibility(false);
        }

        browserWindow.Maximize();

#if DEBUG
        browserWindow.WebContents.OpenDevTools();
#endif

        browserWindow.OnReadyToShow += () => browserWindow.Show();
    });

app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
await app.RunAsync();
