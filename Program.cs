using ElectronNET.API;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("ElectronWebsiteWrapper_");
builder.Services.AddRazorPages();
builder.Services.AddElectron();

builder.UseElectron(args, async () =>
{
    var browserWindow = await Electron.WindowManager.CreateWindowAsync(builder.Configuration["Url"]);
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

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
await app.RunAsync();
