using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a quote or a quote placeholder, with the current authorization status.
/// </summary>
public sealed record Quote
{
    /// <summary>
    /// The state of the quote.
    /// </summary>
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter<QuoteState>))]
    public QuoteState State { get; set; }
    /// <summary>
    /// The status being quoted, if the quote has been accepted. This will be null, unless the state attribute is accepted.
    /// </summary>
    [JsonPropertyName("quoted_status")]
    public Status? QuotedStatus { get; set; }
}

public enum QuoteState
{
    /// <summary>
    /// The quote has not been acknowledged by the quoted account yet, and requires authorization before being displayed.
    /// </summary>
    Pending,
    /// <summary>
    /// The quote has been accepted and can be displayed. This is the only case where status is non-null.
    /// </summary>
    Accepted,
    /// <summary>
    /// The quote has been explicitly rejected by the quoted account, and cannot be displayed.
    /// </summary>
    Rejected,
    /// <summary>
    /// The quote has been previously accepted, but is now revoked, and thus cannot be displayed.
    /// </summary>
    Revoked,
    /// <summary>
    /// The quote has been approved, but the quoted post itself has now been deleted.
    /// </summary>
    Deleted,
    /// <summary>
    /// The quote has been approved, but cannot be displayed because the user is not authorized to see it.
    /// </summary>
    Unauthorized
}
