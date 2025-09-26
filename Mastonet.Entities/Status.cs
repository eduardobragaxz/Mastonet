using Mastonet.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a status posted by an account.
/// </summary>
public record Status
{
    // Base attributes

    /// <summary>
    /// ID of the status in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// URI of the status used for federation.
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; init; } = string.Empty;

    /// <summary>
    /// The date when this status was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The account that authored this status.
    /// </summary>
    [JsonPropertyName("account")]
    public Account Account { get; init; } = new Account();

    /// <summary>
    /// HTML-encoded status content.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; init; } = string.Empty;

    /// <summary>
    /// Visibility of this status.
    /// </summary>
    [JsonPropertyName("visibility")]
    [JsonConverter(typeof(JsonStringEnumConverter<Visibility>))]
    public Visibility Visibility { get; init; }

    /// <summary>
    /// Is this status marked as sensitive content?
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool? Sensitive { get; init; }

    /// <summary>
    /// Subject or summary line, below which status content is collapsed until expanded.
    /// </summary>
    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; init; } = string.Empty;

    /// <summary>
    /// Media that is attached to this status.
    /// </summary>
    [JsonPropertyName("media_attachments")]
    public ImmutableArray<Attachment> MediaAttachments { get; init; } = [];

    /// <summary>
    /// The application used to post this status.
    /// </summary>
    [JsonPropertyName("application")]
    public Application Application { get; init; } = new Application();

    // Rendering attributes

    /// <summary>
    /// Mentions of users within the status content.
    /// </summary>
    [JsonPropertyName("mentions")]
    public ImmutableArray<Mention> Mentions { get; init; } = [];

    /// <summary>
    /// Hashtags used within the status content.
    /// </summary>
    [JsonPropertyName("tags")]
    public ImmutableArray<Tag> Tags { get; init; } = [];

    /// <summary>
    /// Custom emoji to be used when rendering status content.
    /// </summary>
    [JsonPropertyName("emojis")]
    public ImmutableArray<Emoji> Emojis { get; init; } = [];


    // Informational attributes

    /// <summary>
    /// How many boosts this status has received.
    /// </summary>
    [JsonPropertyName("reblogs_count")]
    public long ReblogCount { get; init; }

    /// <summary>
    /// How many favourites this status has received.
    /// </summary>
    [JsonPropertyName("favourites_count")]
    public long FavouritesCount { get; init; }

    /// <summary>
    /// How many replies this status has received.
    /// </summary>
    [JsonPropertyName("replies_count")]
    public long RepliesCount { get; init; }


    // Nullable attributes

    /// <summary>
    /// A link to the status's HTML representation.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// ID of the status being replied.
    /// </summary>
    [JsonPropertyName("in_reply_to_id")]
    public string? InReplyToId { get; init; }

    /// <summary>
    /// ID of the account being replied to.
    /// </summary>
    [JsonPropertyName("in_reply_to_account_id")]
    public string? InReplyToAccountId { get; init; }

    /// <summary>
    /// The status being reblogged.
    /// </summary>
    [JsonPropertyName("reblog")]
    public Status? Reblog { get; init; }

    /// <summary>
    /// The poll attached to the status.
    /// </summary>
    [JsonPropertyName("poll")]
    public Poll? Poll { get; init; }

    /// <summary>
    /// Preview card for links included within status content.
    /// </summary>
    [JsonPropertyName("card")]
    public Card? Card { get; init; }

    /// <summary>
    /// Primary language of this status.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; init; }

    /// <summary>
    /// Plain-text source of a status. 
    /// Returned instead of content when status is deleted, so the user may redraft from the source text 
    /// without the client having to reverse-engineer the original text from the HTML content.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; init; }

    /// <summary>
    /// Information about the status being quoted, if any
    /// </summary>
    [JsonPropertyName("quote")]
    public Quote? Quote { get; init; }
    /// <summary>
    /// Summary of the post quote’s approval policy and how it applies to the user making the request, that is, whether the user can be expected to be allowed to quote that post
    /// </summary>
    [JsonPropertyName("quote_approval")]
    public QuoteApproval? QuoteApproval { get; init; }

    // Authorized user attributes

    /// <summary>
    /// Have you favourited this status?
    /// </summary>
    [JsonPropertyName("favourited")]
    public bool? Favourited { get; set; }

    /// <summary>
    /// Have you boosted this status?
    /// </summary>
    [JsonPropertyName("reblogged")]
    public bool? Reblogged { get; set; }

    /// <summary>
    /// Have you muted notifications for this status's conversation?
    /// </summary>
    [JsonPropertyName("muted")]
    public bool? Muted { get; init; }

    [JsonPropertyName("bookmarked")]
    public bool? Bookmarked { get; set; }

    /// <summary>
    /// Whether the status is pinned
    /// </summary>
    [JsonPropertyName("pinned")]
    public bool? Pinned { get; init; }
}

public class StatusParameters
{
    /// <summary>
    /// The text of the status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Either "direct", "private", "unlisted" or "public"
    /// </summary>
    public Visibility? Visibility { get; set; }
    /// <summary>
    /// ID of the status being quoted, if any
    /// </summary>
    public string? QuotedStatusId { get; set; }
    /// <summary>
    /// Sets who is allowed to quote the status
    /// </summary>
    public QuoteApprovalPolicy? QuoteApprovalPolicy { get; set; }

    /// <summary>
    /// Local ID of the status you want to reply to
    /// </summary>
    public string? ReplyStatusId { get; set; }

    /// <summary>
    /// Array of media IDs to attach to the status (maximum 4)
    /// </summary>
    public IEnumerable<string>? MediaIds { get; set; }

    /// <summary>
    /// Set this to mark the media of the status as NSFW
    /// </summary>
    public bool Sensitive { get; set; }

    /// <summary>
    /// Text to be shown as a warning before the actual content
    /// </summary>
    public string? SpoilerText { get; set; }

    /// <summary>
    /// DateTime to schedule posting of status
    /// </summary>
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// Override language code of the toot (ISO 639-2)
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Nested parameters to attach a poll to the status
    /// </summary>
    public PollParameters? PollParameters { get; set; }
}