using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Models.ISteamUser;

public class PlayerBan
{
    [JsonPropertyName("steamid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong SteamId { get; init; }

    [JsonPropertyName("CommunityBanned")]
    public required bool CommunityBanned { get; init; }

    [JsonPropertyName("VACBanned")]
    public required bool VacBanned { get; init; }

    [JsonPropertyName("NumberOfVACBans")]
    public required int NumberOfVacBans { get; init; }

    [JsonPropertyName("DaysSinceLastBan")]
    public required int DaysSinceLastBan { get; init; }

    [JsonPropertyName("NumberOfGameBans")]
    public required int NumberOfGameBans { get; init; }

    [JsonPropertyName("EconomyBan")]
    public required string EconomyBan { get; init; }
}