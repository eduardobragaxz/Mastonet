using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

public sealed record Profile
{
    /// <summary>
    /// The account id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The profile’s display name.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>
    /// The profile’s bio or description.
    /// </summary>
    [JsonPropertyName("note")]
    public string Note { get; init; } = string.Empty;

    /// <summary>
    /// Metadata about the account.
    /// </summary>
    [JsonPropertyName("fields")]
    public ImmutableArray<Field> Fields { get; init; }

    /// <summary>
    /// An image icon that is shown next to statuses and in the profile.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; init; }

    /// <summary>
    /// A static version of the avatar.
    /// </summary>
    [JsonPropertyName("avatar_static")]
    public string? StaticAvatar { get; init; }

    /// <summary>
    /// A textual description of the avatar, to be used for the visually impaired or when avatars do not load.
    /// </summary>
    [JsonPropertyName("avatar_description")]
    public string AvatarDescription { get; init; } = string.Empty;

    /// <summary>
    /// An image banner that is shown above the profile and in profile cards.
    /// </summary>
    [JsonPropertyName("header")]
    public string? Header { get; init; }

    /// <summary>
    /// A static version of the header.
    /// </summary>
    [JsonPropertyName("header_static")]
    public string? StaticHeader { get; init; }

    /// <summary>
    /// A textual description of the profile header, to be used for the visually impaired or when avatars do not load.
    /// </summary>
    [JsonPropertyName("header_description")]
    public string? HeaderDescription { get; init; }

    /// <summary>
    /// Whether the account manually approves follow requests.
    /// </summary>
    [JsonPropertyName("locked")]
    public bool Locked { get; init; }

    /// <summary>
    /// ndicates that the account may perform automated actions, may not be monitored, or identifies as a robot.
    /// </summary>
    [JsonPropertyName("bot")]
    public bool Bot { get; init; }

    /// <summary>
    /// Whether the user hides the contents of their follows and followers collections.
    /// </summary>
    [JsonPropertyName("hide_collections")]
    public bool? HideCollection { get; init; }

    /// <summary>
    /// Whether the account has opted into discovery features such as the profile directory.
    /// </summary>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; init; }

    /// <summary>
    /// Whether the account allows indexing by search engines.
    /// </summary>
    [JsonPropertyName("indexable")]
    public bool Indexable { get; init; }

    /// <summary>
    /// Whether the account wishes to have a “Media” tab with media attachments on their profile.
    /// </summary>
    [JsonPropertyName("show_media")]
    public bool ShowMedia { get; init; }

    /// <summary>
    /// Whether the account wishes to have replies in the “Media” tab on their profile.
    /// </summary>
    [JsonPropertyName("show_media_replies")]
    public bool ShowMediaReplies { get; init; }

    /// <summary>
    /// hether the account wishes to have a “Featured” tab on their profile.
    /// </summary>
    [JsonPropertyName("show_featured")]
    public bool ShowFeatured { get; init; }

    /// <summary>
    /// hether the account wishes to have a “Featured” tab on their profile.
    /// </summary>
    [JsonPropertyName("attribution_domains")]
    public ImmutableArray<string> AttributionDomains { get; init; }
}