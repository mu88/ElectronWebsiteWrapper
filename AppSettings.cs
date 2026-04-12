namespace ElectronWebsiteWrapper;

internal sealed record AppSettings
{
    public string Url { get; init; } = string.Empty;
}
