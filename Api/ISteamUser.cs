using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Kxnrl.SteamApi.Models.ISteamUser;
using Kxnrl.SteamApi.Responses.ISteamUser;

namespace Kxnrl.SteamApi.Api;

public interface ISteamUser : ISteamApi
{
    /// <summary>
    ///     Get friend list for SteamUser <br />
    ///     <remarks>Throw <see cref="HttpRequestException" /> if user friend list is privacy.</remarks>
    /// </summary>
    Task<PlayerFriend[]> GetFriendList(ulong steamId, string? relationship = null);

    /// <summary>
    ///     Get ban list for SteamUser
    /// </summary>
    Task<PlayerBan[]> GetPlayerBans(ulong[] steamIds);

    /// <summary>
    ///     Get player summary for SteamUser
    /// </summary>
    Task<PlayerSummary[]> GetPlayerSummaries(ulong[] steamIds);
}

internal class SteamUser : SteamApi, ISteamUser
{
    public SteamUser(string apiKey) : base(apiKey)
    {
    }

    public override string GetName()
        => nameof(ISteamUser);

    protected override bool RequiredAuthorized()
        => true;

    public async Task<PlayerFriend[]> GetFriendList(ulong steamId, string? relationship = null)
    {
        var response = await GetRawResponse<PlayerFriendsResponse>($"{nameof(GetFriendList)}/v1", $"steamid={steamId}");

        return response.FriendsList.Friends;
    }

    public Task<PlayerBan[]> GetPlayerBans(ulong[] steamIds)
    {
        var url = $"steamids={string.Join(',', steamIds)}";

        return Get<PlayerBan[]>($"{nameof(GetPlayerBans)}/v1", url, "players");
    }

    public async Task<PlayerSummary[]> GetPlayerSummaries(ulong[] steamIds)
    {
        var url = $"steamids={string.Join(',', steamIds)}";

        var steamChina  = await Get<PlayerSummariesResponse>($"{nameof(GetPlayerSummaries)}/v2", url);
        var steamGlobal = await Get<PlayerSummariesResponse>($"{nameof(GetPlayerSummaries)}/v2", url, false);

        var dict = steamChina.Players.ToDictionary(x => x.SteamId, x => x);

        try
        {
            foreach (var g in steamGlobal.Players)
            {
                dict.TryAdd(g.SteamId, g);
            }
        }
        catch
        {
            // skip
        }

        return [.. dict.Values];
    }
}
