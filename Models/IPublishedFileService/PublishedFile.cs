using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Models.IPublishedFileService;

public class PublishedFile
{
    [JsonPropertyName("publishedfileid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong PublishedFileId { get; init; }
}
