using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kxnrl.SteamApi.Enums;
using Kxnrl.SteamApi.Models.IPublishedFileService;
using Kxnrl.SteamApi.Responses.IPublishedFileService;

namespace Kxnrl.SteamApi.Api;

public interface IPublishedFileService : ISteamApi
{
    /// <summary>
    ///     Get workshop published item details
    /// </summary>
    /// <param name="publishFileIds">Item ID</param>
    /// <param name="includeChildren">Get with children</param>
    /// <returns><see cref="PublishedFileDetails" />, Missing item if private or deleted.</returns>
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
        var response = new List<PublishedFileDetails>(publishFileIds.Length);

        foreach (var chunk in publishFileIds.Distinct().Chunk(50))
        {
            var builder = new StringBuilder();

            builder.Append($"includechildren={includeChildren.ToString().ToLower()}");

            for (var i = 0; i < chunk.Length; i++)
            {
                builder.Append($"&publishedfileids[{i}]={chunk[i]}");
            }

            var chunkResponse = await Get<FileDetailResponse>($"{nameof(GetDetails)}/v1", builder.ToString());

            foreach (var item in chunkResponse.PublishedFileDetails)
            {
                if (item.Result is not SteamApiResult.Ok)
                {
                    Debug.Print($"Ignore Invalid State {JsonSerializer.Serialize(item)}");

                    continue;
                }

                if (item.Creator is null
                    || item.CreatorAppId is null
                    || item.Manifest is null
                    || item.Title is null
                    || item.PreviewImage is null
                    || item.TimeUpdated is null
                    || item.Visibility is null
                    || item.Flags is null
                    || item.FileSize is null
                    || item.FileType is null)
                {
                    Debug.Print($"Ignore Null {JsonSerializer.Serialize(item)}");

                    continue;
                }

                response.Add(new PublishedFileDetails
                {
                    PublishedFileId = item.PublishedFileId,
                    Creator         = item.Creator.Value,
                    CreatorAppId    = item.CreatorAppId.Value,
                    Manifest        = item.Manifest.Value,
                    Title           = item.Title,
                    PreviewImage    = item.PreviewImage,
                    TimeUpdated     = item.TimeUpdated.Value,
                    Visibility      = item.Visibility.Value,
                    Flags           = item.Flags.Value,
                    FileSize        = item.FileSize.Value,
                    FileType        = item.FileType.Value,
                    Children        = item.Children,
                });
            }
        }

        return [.. response];
    }
}
