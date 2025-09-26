using Mastonet.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

public sealed class QuoteApproval
{
    /// <summary>
    /// Describes who is expected to be able to quote that status and have the quote automatically authorized
    /// </summary>
    [JsonPropertyName("automatic")]
    public ImmutableArray<string> Automatic { get; init; }
    /// <summary>
    /// Describes who is expected to have their quotes of this status be manually reviewed by the author before being accepted
    /// </summary>
    [JsonPropertyName("manual")]
    public ImmutableArray<string> Manual { get; init; }
    /// <summary>
    /// Describes how this status’ quote policy applies to the current user
    /// </summary>
    [JsonPropertyName("current_user")]
    [JsonConverter(typeof(JsonStringEnumConverter<CurrentUserQuoteApproval>))]
    public CurrentUserQuoteApproval CurrentUser { get; init; }
}

public enum CurrentUserQuoteApproval
{
    Automatic,
    Manual,
    Denied,
    Unknown
}