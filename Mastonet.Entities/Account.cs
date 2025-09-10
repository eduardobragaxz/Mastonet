using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a user of Mastodon and their associated profile
/// </summary>
public record class Account
{
    // Base Attributes

    /// <summary>
    /// The account id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The username of the account, not including domain.
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; init; } = string.Empty;

    /// <summary>
    /// The Webfinger account URI. Equal to username for local users, or username@domain for remote users.
    /// </summary>
    [JsonPropertyName("acct")]
    public string AccountName { get; init; } = string.Empty;

    /// <summary>
    /// The location of the user's profile page.
    /// </summary>
    [JsonPropertyName("url")]
    public string ProfileUrl { get; init; } = string.Empty;

    // Display attributes

    /// <summary>
    /// The profile's display name.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    /// <summary>
    /// The profile's bio / description.
    /// </summary>
    [JsonPropertyName("note")]
    public string Note { get; init; } = string.Empty;

    /// <summary>
    /// An image icon that is shown next to statuses and in the profile.
    /// </summary>
    [JsonPropertyName("avatar")]
    public string AvatarUrl { get; init; } = string.Empty;

    /// <summary>
    /// A static version of the avatar. Equal to avatar if its value is a static image; different if avatar is an animated GIF.
    /// </summary>
    [JsonPropertyName("avatar_static")]
    public string StaticAvatarUrl { get; init; } = string.Empty;

    /// <summary>
    /// An image banner that is shown above the profile and in profile cards.
    /// </summary>
    [JsonPropertyName("header")]
    public string HeaderUrl { get; init; } = string.Empty;

    /// <summary>
    /// A static version of the header. Equal to header if its value is a static image; different if header is an animated GIF.
    /// </summary>
    [JsonPropertyName("header_static")]
    public string StaticHeaderUrl { get; init; } = string.Empty;

    /// <summary>
    /// Whether the account manually approves follow requests.
    /// </summary>
    [JsonPropertyName("locked")]
    public bool Locked { get; init; }

    /// <summary>
    /// Custom emoji entities to be used when rendering the profile. If none, an empty array will be returned.
    /// </summary>
    [JsonPropertyName("emojis")]
    public ImmutableArray<Emoji> Emojis { get; init; } = [];

    /// <summary>
    /// Whether the account has opted into discovery features such as the profile directory.
    /// </summary>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; init; }

    // Statistical attributes

    /// <summary>
    /// The time the account was created
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// When the most recent status was posted.
    /// </summary>
    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; init; }

    /// <summary>
    /// How many statuses are attached to this account.
    /// </summary>
    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; init; }

    /// <summary>
    /// The reported followers of this profile.
    /// </summary>
    [JsonPropertyName("followers_count")]
    public long FollowersCount { get; init; }

    /// <summary>
    /// The reported follows of this profile.
    /// </summary>
    [JsonPropertyName("following_count")]
    public long FollowingCount { get; init; }

    // Optional attributes

    /// <summary>
    /// Indicates that the profile is currently inactive and that its user has moved to a new account.
    /// </summary>
    [JsonPropertyName("moved")]
    public Account? Moved { get; init; }

    /// <summary>
    /// Additional metadata attached to a profile as name-value pairs.
    /// </summary>
    [JsonPropertyName("fields")]
    public ImmutableArray<Field>? Fields { get; init; }

    /// <summary>
    /// A presentational flag. Indicates that the account may perform automated actions, may not be monitored, or identifies as a robot.
    /// </summary>
    [JsonPropertyName("bot")]
    public bool? Bot { get; init; }

    /// <summary>
    /// An extra entity to be used with API methods to verify credentials and update credentials.
    /// </summary>
    [JsonPropertyName("source")]
    public Source? Source { get; init; }

    /// <summary>
    /// An extra entity returned when an account is suspended.
    /// </summary>
    [JsonPropertyName("suspended")]
    public bool Suspended { get; init; }

    /// <summary>
    /// An extra attribute returned only when an account is silenced. If true, indicates that the account should be hidden behind a warning screen.
    /// </summary>
    [JsonPropertyName("limited")]
    public bool Limited { get; init; }

    /// <summary>
    /// When a timed mute will expire, if applicable.
    /// </summary>
    [JsonPropertyName("mute_expires_at")]
    public DateTime? MuteExpiresAt { get; init; }
}
