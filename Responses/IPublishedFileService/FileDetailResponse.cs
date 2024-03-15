using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Models.IPublishedFileService;

namespace Kxnrl.SteamApi.Responses.IPublishedFileService;

internal class FileDetailResponse
{
    [JsonPropertyName("publishedfiledetails")]
    public required PublishedFileDetails[] PublishedFileDetails { get; init; }
}
