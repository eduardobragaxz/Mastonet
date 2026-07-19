using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet.Entities;

public record Collection
{
    /// <summary>
    /// The collection id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The id of the account that curates this Collection.
    /// </summary>
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// The Collection’s ActivityPub identifier (used for federation).
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// The url of the Collection’s HTML page (web interface URL).
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// The name of the Collection.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// An optional description of the Collection.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Primary language of this Collection.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    ///  Whether the Collection was created on this server or resides on a remote server.
    /// </summary>
    [JsonPropertyName("local")]
    public bool Local { get; set; }

    /// <summary>
    ///  Whether the Collection has been marked as including sensitive content.
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; }

    /// <summary>
    ///  Whether the Collection should show up on the owner’s profile, in search results and recommendations.
    /// </summary>
    [JsonPropertyName("discoverable")]
    public bool Discoverable { get; set; }

    /// <summary>
    ///  A single hashtag that describes this Collection.
    /// </summary>
    [JsonPropertyName("tag")]
    public Tag? Tag { get; set; }

    /// <summary>
    ///  When the Collection was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///  When the Collection was last updated.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///  The number of items in this Collection.
    /// </summary>
    [JsonPropertyName("item_count")]
    public int ItemCount { get; set; }

    /// <summary>
    ///  The items in this Collection.
    /// </summary>
    [JsonPropertyName("items")]
    public ImmutableArray<CollectionItem> Items { get; set; }
}

public record Collections([property: JsonPropertyName("collections")] ImmutableArray<Collection> Items);

public record CollectionItem
{
    /// <summary>
    /// The item id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The id of the account this item represents.
    /// </summary>
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// The current state of the item.
    /// </summary>
    [JsonPropertyName("state")]
    [JsonConverter(typeof(JsonStringEnumConverter<CollectionItemState>))]
    public CollectionItemState State { get; set; }

    /// <summary>
    ///  When the item was added to the collection.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}

public record CollectionWithAccounts
{
    /// <summary>
    /// Full account entities for the owner of the Collection and every account within the Collection.
    /// </summary>
    [JsonPropertyName("accounts")]
    public ImmutableArray<Account> Accounts { get; init; }
    /// <summary>
    /// The actual Collection.
    /// </summary>
    [JsonPropertyName("collection")]
    public Collection? Collection { get; init; }
}

public enum CollectionItemState
{
    Pending,
    Accepted,
    Rejected,
    Revoked
}