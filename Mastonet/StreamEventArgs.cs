using Mastonet.Entities;
using System;

namespace Mastonet;

public class StreamUpdateEventArgs(Status status) : EventArgs
{
    public Status Status { get; set; } = status;
}

public class StreamNotificationEventArgs(Notification notification) : EventArgs
{
    public Notification Notification { get; set; } = notification;
}

public class StreamDeleteEventArgs(long statusId) : EventArgs
{
    public long StatusId { get; set; } = statusId;
}

public class StreamFiltersChangedEventArgs : EventArgs
{
}

public class StreamConversationEvenTargs(Conversation conversation) : EventArgs
{
    public Conversation Conversation { get; set; } = conversation;
}
