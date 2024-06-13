using ElectronNET.API;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);
builder.Services.AddElectron();

WebApplication app = builder.Build();
await app.StartAsync();

await Electron.WindowManager.CreateWindowAsync(app.Configuration["Url"]);

app.WaitForShutdown();