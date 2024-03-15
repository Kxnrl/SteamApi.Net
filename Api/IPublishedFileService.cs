using System.Collections.Generic;
using System.Linq;
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

        foreach (var chunk in publishFileIds.Chunk(50))
        {
            var builder = new StringBuilder();

            builder.Append($"includechildren={includeChildren.ToString().ToLower()}");

            for (var i = 0; i < chunk.Length; i++)
            {
                builder.Append($"&publishedfileids[{i}]={chunk[i]}");
            }

            var chunkResponse = await Get<FileDetailResponse>($"{nameof(GetDetails)}/v1", builder.ToString());

            response.AddRange(chunkResponse.PublishedFileDetails);
        }

        return [.. response];
    }
}
