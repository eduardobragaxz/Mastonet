//using System;
//using System.Collections.Generic;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace Mastonet.Entities.Enums;

public enum NotificationType
{
    None,
    Mention,
    Status,
    Reblog,
    Follow,
    Follow_request,
    Favourite,
    Poll,
    Update,
    Quote,
    Quoted_update
}