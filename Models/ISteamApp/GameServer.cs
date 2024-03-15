using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Models.ISteamApp;

public class GameServer
{
    [JsonPropertyName("addr")]
    public required string Address { get; init; }

    [JsonPropertyName("gmsindex")]
    public required int GmsIndex { get; init; }

    [JsonPropertyName("steamid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong SteamId { get; init; }

    [JsonPropertyName("appid")]
    public required uint AppId { get; init; }

    [JsonPropertyName("gamedir")]
    public required string GameDir { get; init; }

    [JsonPropertyName("region")]
    public required int Region { get; init; }

    [JsonPropertyName("secure")]
    public bool Secure { get; init; }

    [JsonPropertyName("lan")]
    public bool Lan { get; init; }

    [JsonPropertyName("gameport")]
    public required int GamePort { get; init; }

    [JsonPropertyName("specport")]
    public int SpecPort { get; init; }
}