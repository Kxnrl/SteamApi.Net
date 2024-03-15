using System;
using System.Text;
using System.Threading.Tasks;
using Kxnrl.SteamApi.Models.IPublishedFileService;
using Kxnrl.SteamApi.Responses.IPublishedFileService;

namespace Kxnrl.SteamApi.Api;

public interface IPublishedFileService : ISteamApi
{
    /// <summary>
    ///     Get workshop published item details
    /// </summary>
    /// <param name="publishFileIds">Item ID (Max 100 items)</param>
    /// <param name="includeChildren">Get with children</param>
    Task<PublishedFileDetails[]> GetDetails(ulong[] publishFileIds, bool includeChildren = false);
}

internal class PublishedFileService : SteamApi, IPublishedFileService
{
    public PublishedFileService(string apiKey) : base(apiKey)
    {
    }

    public static PublishedFileService Create(string apiKey)
        => new (apiKey);

    public override string GetName()
        => nameof(IPublishedFileService);

    protected override bool RequiredAuthorized()
        => true;

    public async Task<PublishedFileDetails[]> GetDetails(ulong[] publishFileIds, bool includeChildren = false)
    {
        if (publishFileIds.Length > 100)
        {
            throw new ArgumentException("Max 100 items", nameof(publishFileIds));
        }

        var builder = new StringBuilder();

        builder.Append($"includechildren={includeChildren.ToString().ToLower()}");

        for (var i = 0; i < publishFileIds.Length; i++)
        {
            builder.Append($"&publishedfileids[{i}]={publishFileIds[i]}");
        }

        var response = await Get<FileDetailResponse>($"{nameof(GetDetails)}/v1", builder.ToString());

        return response.PublishedFileDetails;
    }
}
