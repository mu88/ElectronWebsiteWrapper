# ElectronWebsiteWrapper

[![Build](https://github.com/mu88/ElectronWebsiteWrapper/actions/workflows/CI_CD.yml/badge.svg)](https://github.com/mu88/ElectronWebsiteWrapper/actions/workflows/CI_CD.yml)
[![Latest Release](https://img.shields.io/github/v/release/mu88/ElectronWebsiteWrapper)](https://github.com/mu88/ElectronWebsiteWrapper/releases/latest)
[![License](https://img.shields.io/badge/license-Do%20No%20Harm-blue)](LICENSE.md)

An app based on [Electron.NET](https://github.com/ElectronNET/Electron.NET/) that wraps any website into a dedicated desktop executable. The primary motivation is to **exclude the executable from clipboard managers** (e.g. Ditto), so that secrets shown on the wrapped site (e.g. a HashiCorp Vault UI) are never captured in clipboard history.

## Download

Pre-built Windows x64 binaries are available on the [**Releases page**](https://github.com/mu88/ElectronWebsiteWrapper/releases/latest). Each release ships a portable `.exe` — no installer required.

## Prerequisites

To **build** the application from source you need:

- .NET SDK — see [`global.json`](global.json) for the required version
- [Node.js](https://nodejs.org/) — required by Electron.NET at build time; see [Electron.NET system requirements](https://github.com/ElectronNET/Electron.NET/wiki/System-Requirements) for the minimum version

To **run** a pre-built binary you only need a Windows x64 machine (no .NET runtime required — the app is self-contained).

## Building for Windows

```
dotnet publish ElectronWebsiteWrapper.csproj -p:PublishProfile=win-x64
```

This restores all dependencies (including Node/Electron) and produces a self-contained portable `.exe` under `publish\Release\`.

### Proxy issues

Electron.NET has known issues behind corporate proxies. Setting `HTTP_PROXY` / `HTTPS_PROXY` is not always sufficient. Configuring the NPM proxy via `npm config set proxy http://my.company.com:3128` helps partially but may still fail for certain Electron packages. The only reliable workaround is to build in a network environment without a proxy.

## Configuration

The app uses the standard [.NET Configuration](https://learn.microsoft.com/dotnet/core/extensions/configuration) system. Configuration can be provided via `appsettings.json` **or** environment variables.

### `appsettings.json`

```json
{
  "Url": "https://vault.example.com"
}
```

| Key | Required | Description |
|-----|----------|-------------|
| `Url` | ✅ Yes | The website to wrap. Must be a valid absolute URI (e.g. `https://…`). The app fails fast with a descriptive error if this value is missing or invalid. |

### Environment variable

The prefix `ElectronWebsiteWrapper_` maps to configuration keys:

```
ElectronWebsiteWrapper_Url=https://vault.example.com
```

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a full history of releases and changes.
