using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents the tree around a given status. Used for reconstructing threads of statuses.
/// </summary>
public sealed record Context
{
    /// <summary>
    /// Parents in the thread.
    /// </summary>
    [JsonPropertyName("ancestors")]
    public ImmutableArray<Status> Ancestors { get; set; } = [];

    /// <summary>
    /// Children in the thread.
    /// </summary>
    [JsonPropertyName("descendants")]
    public ImmutableArray<Status> Descendants { get; set; } = [];
}