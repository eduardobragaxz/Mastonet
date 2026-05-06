using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

/// <summary>
/// Represents a file or media attachment that can be added to a status.
/// </summary>
public sealed record Attachment
{
    /// <summary>
    /// The ID of the attachment in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The type of the attachment.
    /// One of: "unknown", "image", "gifv", "video", "audio"
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter<AttachmentType>))]
    public AttachmentType Type { get; init; }

    /// <summary>
    /// The location of the original full-size attachment.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// The location of a scaled-down preview of the attachment.
    /// </summary>
    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; init; }

    /// <summary>
    /// The location of the full-size original attachment on the remote website.
    /// </summary>
    [JsonPropertyName("remote_url")]
    public string? RemoteUrl { get; init; }

    ///<summary>
    /// Metadata returned by Paperclip.
    ///</summary>
    [JsonPropertyName("meta")]
    public AttachmentMeta? Meta { get; init; }

    /// <summary>
    /// Alternate text that describes what is in the media attachment, to be used for the visually 
    /// impaired or when media attachments do not load.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// A hash computed by the BlurHash algorithm, for generating colorful preview thumbnails when 
    /// media has not been downloaded yet.
    /// </summary>
    [JsonPropertyName("blurhash")]
    public string? BlurHash { get; init; }

    /// <summary>
    /// A shorter URL for the attachment.
    /// </summary>
    [JsonPropertyName("text_url")]
    [Obsolete("Attribute was deprecated in version 3.5.0")]
    public string? TextUrl { get; init; }
}

public class AttachmentMeta
{
    [JsonPropertyName("original")]
    public AttachmentSizeData? Original { get; init; }

    [JsonPropertyName("small")]
    public AttachmentSizeData? Small { get; init; }

    [JsonPropertyName("focus")]
    public AttachmentFocusData? Focus { get; init; }
}

public class AttachmentSizeData
{

    [JsonPropertyName("width")]
    public int? Width { get; init; }

    [JsonPropertyName("height")]
    public int? Height { get; init; }


    [JsonPropertyName("size")]
    public string? Size { get; init; }

    [JsonPropertyName("aspect")]
    public double? Aspect { get; init; }

    [JsonPropertyName("frame_rate")]
    public string? FrameRate { get; init; }

    [JsonPropertyName("duration")]
    public double? Duration { get; init; }

    [JsonPropertyName("bitrate")]
    public int? BitRate { get; init; }
}

public class AttachmentFocusData
{
    [JsonPropertyName("x")]
    [JsonConverter(typeof(NullToDoubleConverter))]
    public double? X { get; init; }

    [JsonPropertyName("y")]
    [JsonConverter(typeof(NullToDoubleConverter))]
    public double? Y { get; init; }
}
public enum AttachmentType
{
    Image,
    Gifv,
    Video,
    Audio,
    Unknown
}
public class NullToDoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType == JsonTokenType.Null ? 0 : reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}