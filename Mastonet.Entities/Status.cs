﻿using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastonet.Entities;

/// <summary>
/// Represents a status posted by an account.
/// </summary>
public class Status
{
    // Base attributes

    /// <summary>
    /// ID of the status in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// URI of the status used for federation.
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// The date when this status was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The account that authored this status.
    /// </summary>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new Account();

    /// <summary>
    /// HTML-encoded status content.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Visibility of this status.
    /// </summary>
    [JsonPropertyName("visibility")]
    public Visibility Visibility { get; set; }

    /// <summary>
    /// Is this status marked as sensitive content?
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool? Sensitive { get; set; }

    /// <summary>
    /// Subject or summary line, below which status content is collapsed until expanded.
    /// </summary>
    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    /// <summary>
    /// Media that is attached to this status.
    /// </summary>
    [JsonPropertyName("media_attachments")]
    public IEnumerable<Attachment> MediaAttachments { get; set; } = Enumerable.Empty<Attachment>();

    /// <summary>
    /// The application used to post this status.
    /// </summary>
    [JsonPropertyName("application")]
    public Application Application { get; set; } = new Application();

    // Rendering attributes

    /// <summary>
    /// Mentions of users within the status content.
    /// </summary>
    [JsonPropertyName("mentions")]
    public IEnumerable<Mention> Mentions { get; set; } = Enumerable.Empty<Mention>();

    /// <summary>
    /// Hashtags used within the status content.
    /// </summary>
    [JsonPropertyName("tags")]
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();

    /// <summary>
    /// Custom emoji to be used when rendering status content.
    /// </summary>
    [JsonPropertyName("emojis")]
    public IEnumerable<Emoji> Emojis { get; set; } = Enumerable.Empty<Emoji>();


    // Informational attributes

    /// <summary>
    /// How many boosts this status has received.
    /// </summary>
    [JsonPropertyName("reblogs_count")]
    public long ReblogCount { get; set; }

    /// <summary>
    /// How many favourites this status has received.
    /// </summary>
    [JsonPropertyName("favourites_count")]
    public long FavouritesCount { get; set; }

    /// <summary>
    /// How many replies this status has received.
    /// </summary>
    [JsonPropertyName("replies_count")]
    public long RepliesCount { get; set; }


    // Nullable attributes

    /// <summary>
    /// A link to the status's HTML representation.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// ID of the status being replied.
    /// </summary>
    [JsonPropertyName("in_reply_to_id")]
    public string? InReplyToId { get; set; }

    /// <summary>
    /// ID of the account being replied to.
    /// </summary>
    [JsonPropertyName("in_reply_to_account_id")]
    public string? InReplyToAccountId { get; set; }

    /// <summary>
    /// The status being reblogged.
    /// </summary>
    [JsonPropertyName("reblog")]
    public Status? Reblog { get; set; }

    /// <summary>
    /// The poll attached to the status.
    /// </summary>
    [JsonPropertyName("poll")]
    public Poll? Poll { get; set; }

    /// <summary>
    /// Preview card for links included within status content.
    /// </summary>
    [JsonPropertyName("card")]
    public Card? Card { get; set; }

    /// <summary>
    /// Primary language of this status.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Plain-text source of a status. 
    /// Returned instead of content when status is deleted, so the user may redraft from the source text 
    /// without the client having to reverse-engineer the original text from the HTML content.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }


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
    public bool? Muted { get; set; }

    [JsonPropertyName("bookmarked")]
    public bool? Bookmarked { get; set; }

    /// <summary>
    /// Whether the status is pinned
    /// </summary>
    [JsonPropertyName("pinned")]
    public bool? Pinned { get; set; }
}
