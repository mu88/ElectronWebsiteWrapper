using ElectronNET.API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);
builder.Services.AddElectron();
builder.Configuration.AddEnvironmentVariables(prefix: "ElectronWebsiteWrapper_");

await using var app = builder.Build();
await app.StartAsync();

BrowserWindow browserWindow = await Electron.WindowManager.CreateWindowAsync(app.Configuration["Url"]);
browserWindow.OnPageTitleUpdated += _ => browserWindow.SetTitle("ElectronWebsiteWrapper");
browserWindow.SetMenuBarVisibility(false);
browserWindow.Maximize();

app.WaitForShutdown();