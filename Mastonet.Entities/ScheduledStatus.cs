using Mastonet.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a status that will be published at a future scheduled date.
/// </summary>
public sealed record ScheduledStatus
{
    /// <summary>
    /// ID of the scheduled status in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// DateTime to publish the scheduled status
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; init; }

    /// <summary>
    /// Parameters of the scheduled status
    /// </summary>
    [JsonPropertyName("params")]
    public StatusParams Params { get; init; } = new StatusParams();

    /// <summary>
    /// Media attached to the scheduled status
    /// </summary>
    [JsonPropertyName("media_attachments")]
    public IEnumerable<Attachment> MediaAttachments { get; init; } = [];
}

public class StatusParams
{
    /// <summary>
    /// Content of the status in plain text
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    /// <summary>
    /// null or the ID of the status it replies to
    /// </summary>
    [JsonPropertyName("in_reply_to_id")]
    public int? InReplyToId { get; init; }

    /// <summary>
    /// IDs of the attachments
    /// </summary>
    [JsonPropertyName("media_ids")]
    public ImmutableArray<string>? MediaIds { get; init; }

    /// <summary>
    /// Whether to mark the attachment as sensitive, or null 
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool? Sensitive { get; init; }

    /// <summary>
    /// Spoiler text if any
    /// </summary>
    [JsonPropertyName("spoiler_text")]
    public string? SpoilerText { get; init; }

    /// <summary>
    /// Visibility of the scheduled status
    /// </summary>
    [JsonPropertyName("visibility")]
    [JsonConverter(typeof(JsonStringEnumConverter<Visibility>))]
    public Visibility Visibility { get; init; }

    /// <summary>
    /// DateTime to publish the scheduled status
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime? ScheduledAt { get; init; }

    /// <summary>
    /// Application ID that created the scheduled status
    /// </summary>
    [JsonPropertyName("application_id")]
    [Obsolete("Deprecated")]
    public long ApplicationId { get; init; }
}