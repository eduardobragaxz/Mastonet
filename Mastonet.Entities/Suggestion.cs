using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

public record class Suggestion
{
    /// <summary>
    /// A list of reasons this account is being suggested.
    /// </summary>
    [JsonPropertyName("sources")]
    //[JsonConverter(typeof(JsonStringEnumConverter<SuggestionReasons>))]
    public ImmutableArray<SuggestionReasons> Sources { get; init; }

    /// <summary>
    /// The account being recommended to follow.
    /// </summary>
    [JsonPropertyName("account")]
    public Account? Account { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter<SuggestionReasons>))]
public enum SuggestionReasons
{
    Featured,

    [JsonStringEnumMemberName("most_followed")]
    MostFollowed,

    [JsonStringEnumMemberName("most_interactions")]
    MostInteractions,

    [JsonStringEnumMemberName("similar_to_recently_followed")]
    SimilarToRecentlyFollowed,

    [JsonStringEnumMemberName("friends_of_friends")]
    FriendsOfFriends
}