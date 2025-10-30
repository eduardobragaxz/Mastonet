using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents an error message.
/// </summary>
public sealed record Error
{
    /// <summary>
    /// The error message.
    /// </summary>
    [JsonPropertyName("error")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// A longer description of the error, mainly provided with the OAuth API.
    /// </summary>
    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }
}
