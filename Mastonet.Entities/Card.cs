using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a rich preview card that is generated using OpenGraph tags from a URL.
/// </summary>
public sealed record Card
{
    /// <summary>
    /// Location of linked resource.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;

    /// <summary>
    /// Title of linked resource.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Description of preview.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// The type of the preview card. One of :
    /// link = Link OEmbed
    /// photo = Photo OEmbed
    /// video = Video OEmbed
    /// rich = iframe OEmbed.Not currently accepted, so won't show up in practice.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
    /// <summary>
    /// Fediverse account of the authors of the original resource.
    /// </summary>
    [JsonPropertyName("authors")]
    public ImmutableArray<CardAuthor> Authors { get; init; }

    /// <summary>
    /// The author of the original resource.
    /// </summary>
    [Obsolete("This entity was deprecated in Mastodon v4.3")]
    [JsonPropertyName("author_name")]
    public string? AuthorName { get; init; }

    /// <summary>
    /// A link to the author of the original resource.
    /// </summary>
    [Obsolete("This entity was deprecated in Mastodon v4.3")]
    [JsonPropertyName("author_url")]
    public string? AuthorUrl { get; init; }

    /// <summary>
    /// The provider of the original resource.
    /// </summary>
    [JsonPropertyName("provider_name")]
    public string ProviderName { get; init; } = string.Empty;

    /// <summary>
    /// A link to the provider of the original resource.
    /// </summary>
    [JsonPropertyName("provider_url")]
    public string ProviderUrl { get; init; } = string.Empty;

    /// <summary>
    /// HTML to be used for generating the preview card.
    /// </summary>
    [JsonPropertyName("html")]
    public string Html { get; init; } = string.Empty;

    /// <summary>
    /// Width of preview, in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; init; }

    /// <summary>
    /// Height of preview, in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; init; }

    /// <summary>
    /// Preview thumbnail.
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; init; }

    /// <summary>
    /// Used for photo embeds, instead of custom html.
    /// </summary>
    [JsonPropertyName("embed_url")]
    public string EmbedUrl { get; init; } = string.Empty;

    /// <summary>
    /// A hash computed by the BlurHash algorithm, for generating colorful preview thumbnails 
    /// when media has not been downloaded yet.
    /// </summary>
    [JsonPropertyName("blurhash")]
    public string? BlurHash { get; init; }
}
