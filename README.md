# ElectronWebsiteWrapper

This repo contains an app based on [Electron.NET](https://github.com/ElectronNET/Electron.NET/). Its only purpose is to encapsulate any website into this dedicated app. This way, the executable `ElectronWebsiteWrapper.exe` can be excluded in clipboard tools(e.g. Ditto).  

For example, when wrapping a vault's website (e.g. HashiCorp Vault) inside ElectronWebsiteWrapper and excluding the executable from the clipboard manager, secrets can no longer be revealed by the clipboard manager's history.

## Building the app for Windows

Assuming the .NET SDK is installed, run the following script to build the app:

```
dotnet publish ElectronWebsiteWrapper.csproj -p:PublishProfile=win-x64
```

This will restore all necessary dependencies and build the Electron.NET app.

### Proxy issues

At the time of writing, Electron.NET has several issues when run behind a corporate proxy. One can try to set the regular `HTTP_PROXY` and `HTTPS_PROXY` environment variables, but that didn't work for me.  
Configure the NPM proxy like `npm config set proxy http://my.company.com:3128` at least restored some dependencies, but failed for certain Electron dependencies.

Currently, the only workaround is to not build on an environment where a proxy is needed.

## Configuration

Since it's a .NET app, the default [Configuration in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration) applies.

The configuration parameter `Url` can be used to specify the website to wrap. The environment variable `ElectronWebsiteWrapper_Url` can be used, too.
