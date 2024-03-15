using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Models.ISteamUser;

namespace Kxnrl.SteamApi.Responses.ISteamUser;

internal class PlayerSummariesResponse
{
    [JsonPropertyName("players")]
    public required PlayerSummary[] Players { get; init; }
}