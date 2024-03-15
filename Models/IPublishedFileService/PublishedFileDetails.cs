using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Enums;

namespace Kxnrl.SteamApi.Models.IPublishedFileService;

public class PublishedFileDetails : PublishedFile
{
    [JsonPropertyName("result")]
    public required SteamApiResult Result { get; init; }

    [JsonPropertyName("creator")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong Creator { get; init; }

    [JsonPropertyName("creator_appid")]
    public required int CreatorAppId { get; init; }

    [JsonPropertyName("hcontent_file")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong Manifest { get; init; }

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("preview_url")]
    public string? PreviewImage { get; init; }

    [JsonPropertyName("time_updated")]
    public required int TimeUpdated { get; init; }

    [JsonPropertyName("visibility")]
    public required ItemVisibilityType Visibility { get; init; }

    [JsonPropertyName("flags")]
    public required int Flags { get; init; }

    [JsonPropertyName("children")]
    public PublishedFileDetailsChildren[]? Children { get; init; }

    [JsonIgnore]
    private const int WaitForApproveFlag = 128;

    [JsonIgnore]
    public bool IsWaitingForApprove => (Flags & WaitForApproveFlag) != 0;
}

public class PublishedFileDetailsChildren : PublishedFile
{
    [JsonPropertyName("sortorder")]
    public required int SortOrder { get; init; }
}