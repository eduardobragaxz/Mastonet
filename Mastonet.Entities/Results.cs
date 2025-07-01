using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;


/// <summary>
/// Represents the results of a search.
/// </summary>
public class SearchResults
{
    /// <summary>
    /// Accounts which match the given query
    /// </summary>
    [JsonPropertyName("accounts")]
    public ImmutableArray<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Statuses which match the given query
    /// </summary>
    [JsonPropertyName("statuses")]
    public ImmutableArray<Status> Statuses { get; set; } = [];

    /// <summary>
    /// Hashtags which match the given query
    /// </summary>
    [JsonPropertyName("hashtags")]
    public ImmutableArray<Tag> Hashtags { get; set; } = [];
}