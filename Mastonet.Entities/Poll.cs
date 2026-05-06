using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a poll attached to a status.
/// </summary>
public sealed record Poll
{
    /// <summary>
    /// The ID of the poll in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// When the poll ends.
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; init; }

    /// <summary>
    /// Is the poll currently expired?
    /// </summary>
    [JsonPropertyName("expired")]
    public bool Expired { get; init; }

    /// <summary>
    /// Does the poll allow multiple-choice answers?
    /// </summary>
    [JsonPropertyName("multiple")]
    public bool Multiple { get; init; }

    /// <summary>
    /// How many votes have been received.
    /// </summary>
    [JsonPropertyName("votes_count")]
    public int VotesCount { get; init; }

    /// <summary>
    /// How many unique accounts have voted on a multiple-choice poll.
    /// null if Multiple is false
    /// </summary>
    [JsonPropertyName("voters_count")]
    public int? VotersCount { get; init; }

    /// <summary>
    /// When called with a user token, has the authorized user voted?
    /// </summary>
    [JsonPropertyName("voted")]
    public bool? Voted { get; init; }

    /// <summary>
    /// When called with a user token, which options has the authorized user chosen? 
    /// Contains an array of index values for options.
    /// </summary>
    [JsonPropertyName("own_votes")]
    public ImmutableArray<int>? OwnVotes { get; init; }

    /// <summary>
    /// Possible answers for the poll.
    /// </summary>
    [JsonPropertyName("options")]
    public ImmutableArray<PollOption> Options { get; init; }

    /// <summary>
    /// Custom emoji to be used for rendering poll options.
    /// </summary>
    [JsonPropertyName("emojis")]
    public ImmutableArray<Emoji> Emojis { get; init; }
}

public class PollOption
{
    /// <summary>
    /// The text value of the poll option. 
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// The number of received votes for this option. Number, or null if results are not published yet.
    /// </summary>
    [JsonPropertyName("votes_count")]
    public int? VotesCount { get; init; }
}

public class PollParameters
{
    /// <summary>
    /// The array of options
    /// </summary>
    public IEnumerable<string>? Options { get; set; }

    /// <summary>
    /// The timespan until expiration
    /// </summary>
    public TimeSpan ExpiresIn { get; set; }

    /// <summary>
    /// Whether to accept a vote for multiple options
    /// </summary>
    public bool? Multiple { get; set; }

    /// <summary>
    /// Whether to hide the number of votes for each option until expiration
    /// </summary>
    public bool? HideTotals { get; set; }
}
