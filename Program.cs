using ElectronNET.API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);
builder.Services.AddElectron();
builder.Configuration.AddEnvironmentVariables(prefix: "ElectronWebsiteWrapper_");

WebApplication app = builder.Build();
await app.StartAsync();

BrowserWindow browserWindow = await Electron.WindowManager.CreateWindowAsync(app.Configuration["Url"]);
browserWindow.SetMenuBarVisibility(false);
browserWindow.Maximize();

app.WaitForShutdown();