using System.Text.Json.Serialization;

namespace Mastonet.Entities;

public sealed record CardAuthor
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    [JsonPropertyName("account")]
    public Account? Account { get; init; }
}
