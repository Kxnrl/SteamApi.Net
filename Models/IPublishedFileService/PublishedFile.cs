using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Enums;

namespace Kxnrl.SteamApi.Models.IPublishedFileService;

public class PublishedFile
{
    [JsonPropertyName("publishedfileid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong PublishedFileId { get; init; }

    [JsonPropertyName("file_type")]
    public required ItemFileType FileType { get; init; }
}
