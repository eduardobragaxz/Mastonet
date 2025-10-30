using System.Text.Json.Serialization;

namespace Mastonet.Entities;

public sealed record CardAuthor
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("account")]
    public Account? Account { get; set; }
}
