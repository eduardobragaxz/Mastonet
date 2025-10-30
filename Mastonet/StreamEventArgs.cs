using Mastonet.Entities;
using System;

namespace Mastonet;

public sealed class StreamUpdateEventArgs(Status status) : EventArgs
{
    public Status Status { get; set; } = status;
}

public sealed class StreamNotificationEventArgs(Notification notification) : EventArgs
{
    public Notification Notification { get; set; } = notification;
}

public sealed class StreamDeleteEventArgs(long statusId) : EventArgs
{
    public long StatusId { get; set; } = statusId;
}

public sealed class StreamFiltersChangedEventArgs : EventArgs
{
}

public sealed class StreamConversationEvenTargs(Conversation conversation) : EventArgs
{
    public Conversation Conversation { get; set; } = conversation;
}
