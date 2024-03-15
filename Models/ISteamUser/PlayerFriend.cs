using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Models.ISteamUser;

public class PlayerFriend
{
    [JsonPropertyName("steamid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong SteamId { get; init; }

    [JsonPropertyName("relationship")]
    public required string Relationship { get; init; }

    [JsonPropertyName("friend_since")]
    public required int FriendSince { get; init; }
}