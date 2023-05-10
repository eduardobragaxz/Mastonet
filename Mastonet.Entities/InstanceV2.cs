﻿using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastonet.Entities;

/// <summary>
/// Represents the software instance of Mastodon running on this domain.
/// </summary>
public class InstanceV2
{
    /// <summary>
    /// The domain name of the instance.
    /// </summary>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// The title of the website.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The version of Mastodon installed on the instance.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// The URL for the source code of the software running on this instance, in keeping with AGPL license requirements.
    /// </summary>
    [JsonPropertyName("source_url")]
    public string SourceUrl { get; set; } = string.Empty;

    /// <summary>
    /// Admin-defined description of the Mastodon site.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Usage data for this instance.
    /// </summary>
    [JsonPropertyName("usage")]
    public InstanceUsage Usage { get; set; }

    /// <summary>
    /// An image used to represent this instance.
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public InstanceThumbnail Thumbnail { get; set; }

    /// <summary>
    /// Primary languages of the website and its staff.
    /// </summary>
    [JsonPropertyName("languages")]
    public IEnumerable<string> Languages { get; set; } = Enumerable.Empty<string>();

    /// <summary>
    /// Configured values and limits for this website.
    /// </summary>
    [JsonPropertyName("configuration")]
    public InstanceConfiguration Configuration { get; set; }

    /// <summary>
    /// Information about registering for this website.
    /// </summary>
    [JsonPropertyName("registrations")]
    public InstanceRegistrations  Registrations { get; set; }
    
    /// <summary>
    /// Hints related to contacting a representative of the website.
    /// </summary>
    [JsonPropertyName("contact")]
    public InstanceContact Contact { get; set; }
    
    /// <summary>
    /// An itemized list of rules for this website.
    /// </summary>
    [JsonPropertyName("rules")]
    public IEnumerable< InstanceRule> Rules { get; set; }
}

public class InstanceUsage
{
    /// <summary>
    /// Usage data related to users on this instance. 
    /// </summary>
    [JsonPropertyName("users")]
    public InstanceUsageUsers Users { get; set; }
}

public class InstanceUsageUsers
{
    /// <summary>
    /// The number of active users in the past 4 weeks.
    /// </summary>
    [JsonPropertyName("active_month")]
    public int ActiveMonth { get; set; }
}

public class InstanceThumbnail
{
    /// <summary>
    /// The URL for the thumbnail image.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    /// A hash computed by the BlurHash algorithm, for generating colorful preview thumbnails when media has not been downloaded yet.
    /// </summary>
    [JsonPropertyName("blurhash")]
    public string? BlurHash { get; set; }

    /// <summary>
    /// Links to scaled resolution images, for high DPI screens.
    /// </summary>
    [JsonPropertyName("versions")]
    public InstanceThumbnailVersions? Versions { get; set; }
}

public class InstanceThumbnailVersions
{
    /// <summary>
    /// The URL for the thumbnail image at 1x resolution.
    /// </summary>
    [JsonPropertyName("@1x")]
    public string? Res1x { get; set; }

    /// <summary>
    /// The URL for the thumbnail image at 2x resolution.
    /// </summary>
    [JsonPropertyName("@2x")]
    public string? Res2x { get; set; }
}

public class InstanceRegistrations
{
    /// <summary>
    /// Whether registrations are enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Whether registrations require moderator approval.
    /// </summary>
    [JsonPropertyName("approval_required")]
    public bool ApprovalRequired { get; set; }

    /// <summary>
    /// A custom message (HTML) to be shown when registrations are closed.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class InstanceContact
{
    /// <summary>
    /// An email address that can be messaged regarding inquiries or issues.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    /// <summary>
    /// An account that can be contacted natively over the network regarding inquiries or issues.
    /// </summary>
    [JsonPropertyName("account")] 
    public Account Account { get; set; }
}

public class InstanceRule
{
    /// <summary>
    /// An identifier for the rule.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// The rule to be followed.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; }
}