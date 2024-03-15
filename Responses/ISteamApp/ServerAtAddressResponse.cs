using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Models.ISteamApp;

namespace Kxnrl.SteamApi.Responses.ISteamApp;

internal class ServerAtAddressResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    [JsonPropertyName("servers")]
    public required GameServer[] Servers { get; init; }
}
