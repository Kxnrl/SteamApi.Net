using System.Text.Json.Serialization;

namespace Kxnrl.SteamApi.Responses;

internal class SteamApiResponse<T> where T : class
{
    [JsonPropertyName("response")]
    public required T Response { get; init; }
}
