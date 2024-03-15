using System;
using System.Threading.Tasks;
using Kxnrl.SteamApi.Models.ISteamApp;
using Kxnrl.SteamApi.Responses.ISteamApp;

namespace Kxnrl.SteamApi.Api;

public interface ISteamApps : ISteamApi
{
    /// <summary>
    ///     Get GameServer at address
    /// </summary>
    Task<GameServer[]> GetServersAtAddress(string address);

    /// <summary>
    ///     Check version is up-to-date
    /// </summary>
    /// <param name="appId">App ID</param>
    /// <param name="version">Version of app, must greater than 0</param>
    Task<UpToDate> UpToDateCheck(uint appId, uint version);
}

internal class SteamApps : SteamApi, ISteamApps
{
    public SteamApps(string apiKey) : base(apiKey)
    {
    }

    public override string GetName()
        => nameof(ISteamApps);

    protected override bool RequiredAuthorized()
        => false;

    public async Task<GameServer[]> GetServersAtAddress(string address)
    {
        var url = $"addr={address}";

        var response = await Get<ServerAtAddressResponse>($"{nameof(GetServersAtAddress)}/v1", url);

        return response.Servers;
    }

    public Task<UpToDate> UpToDateCheck(uint appId, uint version)
    {
        if (version == 0)
        {
            throw new ArgumentException("Version must greater than 0", nameof(version));
        }

        var url = $"appid={appId}&version={version}";

        return Get<UpToDate>($"{nameof(UpToDateCheck)}/v1", url);
    }
}