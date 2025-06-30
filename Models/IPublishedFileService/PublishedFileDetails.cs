using System.Text.Json.Serialization;
using Kxnrl.SteamApi.Enums;

namespace Kxnrl.SteamApi.Models.IPublishedFileService;

internal class PublishedFileDetailsInternal : PublishedFile
{
    [JsonPropertyName("result")]
    public SteamApiResult Result { get; init; }

    [JsonPropertyName("creator")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public ulong? Creator { get; init; }

    [JsonPropertyName("creator_appid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public long? CreatorAppId { get; init; }

    [JsonPropertyName("hcontent_file")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public ulong? Manifest { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("preview_url")]
    public string? PreviewImage { get; init; }

    [JsonPropertyName("time_updated")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public long? TimeUpdated { get; init; }

    [JsonPropertyName("visibility")]
    public ItemVisibilityType? Visibility { get; init; }

    [JsonPropertyName("flags")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public long? Flags { get; init; }

    [JsonPropertyName("file_size")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public long? FileSize { get; init; }

    [JsonPropertyName("file_type")]
    public ItemFileType? FileType { get; init; }

    [JsonPropertyName("children")]
    public PublishedFileDetailsChildren[]? Children { get; init; }
}

public class PublishedFileDetails : PublishedFile
{
    [JsonPropertyName("creator")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong Creator { get; init; }

    [JsonPropertyName("creator_appid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required long CreatorAppId { get; init; }

    [JsonPropertyName("hcontent_file")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required ulong Manifest { get; init; }

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("preview_url")]
    public required string PreviewImage { get; init; }

    [JsonPropertyName("time_updated")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required long TimeUpdated { get; init; }

    [JsonPropertyName("visibility")]
    public required ItemVisibilityType Visibility { get; init; }

    [JsonPropertyName("flags")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required long Flags { get; init; }

    [JsonPropertyName("file_size")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required long FileSize { get; init; }

    [JsonPropertyName("file_type")]
    public required ItemFileType FileType { get; init; }

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
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public required long SortOrder { get; init; }

    [JsonPropertyName("file_type")]
    public required ItemFileType FileType { get; init; }
}
