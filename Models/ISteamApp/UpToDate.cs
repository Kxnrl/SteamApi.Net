using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Models.ISteamApp;

public class UpToDate
{
    [JsonPropertyName("up_to_date")]
    public required bool LatestVersion { get; init; }

    [JsonPropertyName("version_is_listable")]
    public required bool VersionIsListable { get; init; }

    [JsonPropertyName("required_version")]
    public uint RequiredVersion { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}
