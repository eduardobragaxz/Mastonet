using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents the tree around a given status. Used for reconstructing threads of statuses.
/// </summary>
public class Context
{
    /// <summary>
    /// Parents in the thread.
    /// </summary>
    [JsonPropertyName("ancestors")]
    public IEnumerable<Status> Ancestors { get; set; } = [];

    /// <summary>
    /// Children in the thread.
    /// </summary>
    [JsonPropertyName("descendants")]
    public IEnumerable<Status> Descendants { get; set; } = [];
}
