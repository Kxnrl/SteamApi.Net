using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Models.ISteamUser;

public class PlayerSummary
{
    [JsonPropertyName("steamid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong SteamId { get; init; }

    [JsonPropertyName("communityvisibilitystate")]
    public required int VisibilityState { get; init; }

    [JsonPropertyName("personaname")]
    public required string PersonaName { get; init; }

    [JsonPropertyName("avatar")]
    public required string Avatar { get; init; }

    [JsonPropertyName("avatarhash")]
    public required string AvatarHash { get; init; }

    [JsonPropertyName("personastate")]
    public int PersonaState { get; init; }

    [JsonPropertyName("personastateflags")]
    public int PersonaStateFlags { get; init; }

    [JsonPropertyName("gameextrainfo")]
    public string? GameExtraInfo { get; init; }

    [JsonPropertyName("gameid")]
    public string? GameId { get; init; }
}