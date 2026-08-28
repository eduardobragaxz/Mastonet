using System.Text.Json.Serialization;

namespace Mastonet.Entities.Enums;

public enum NotificationType
{
    None,
    Mention,
    Status,
    Reblog,
    Follow,

    [JsonStringEnumMemberName("follow_request")]
    FollowRequest,
    Favourite,
    Poll,
    Update,
    Quote,

    [JsonStringEnumMemberName("quoted_update")]
    QuotedUpdate
}