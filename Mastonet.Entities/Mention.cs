﻿using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastonet.Entities;

/// <summary>
/// Represents a mention of a user within the content of a status.
/// </summary>
public class Mention
{
    /// <summary>
    /// The account id of the mentioned user.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The username of the mentioned user.
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// The webfinger acct: URI of the mentioned user. 
    /// Equivalent to username for local users, or username@domain for remote users.
    /// </summary>
    [JsonPropertyName("acct")]
    public string AccountName { get; set; } = string.Empty;

    /// <summary>
    /// The location of the mentioned user's profile.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

}
