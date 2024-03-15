using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Models.ISteamUser;

namespace Kxnrl.SteamApi.Responses.ISteamUser;

internal class PlayerFriendsResponse
{
    [JsonPropertyName("friendslist")]
    public required PlayerFriendsList FriendsList { get; init; }

    internal class PlayerFriendsList
    {
        [JsonPropertyName("friends")]
        public required PlayerFriend[] Friends { get; init; }
    }
}
