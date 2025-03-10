using Mastonet.Entities;
using Mastonet.Entities.Enums;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mastonet;

public abstract class TimelineStreaming(StreamingType type, string? param, string? accessToken)
{
    protected readonly StreamingType streamingType = type;
    protected readonly string? param = param;
    protected readonly string? accessToken = accessToken;

    public event EventHandler<StreamUpdateEventArgs>? OnUpdate;
    public event EventHandler<StreamNotificationEventArgs>? OnNotification;
    public event EventHandler<StreamDeleteEventArgs>? OnDelete;
    public event EventHandler<StreamFiltersChangedEventArgs>? OnFiltersChanged;
    public event EventHandler<StreamConversationEvenTargs>? OnConversation;

    public abstract Task Start();
    public abstract void Stop();

    protected void SendEvent(string eventName, string data)
    {
        switch (eventName)
        {
            case "update":
                var status = JsonSerializer.Deserialize(data, TryDeserializeContext.Default.Status);

                if (status is not null)
                {
                    OnUpdate?.Invoke(this, new StreamUpdateEventArgs(status));
                }
                break;
            case "notification":

                var notification = JsonSerializer.Deserialize(data, TryDeserializeContext.Default.Notification);

                if (notification is not null)
                {
                    OnNotification?.Invoke(this, new StreamNotificationEventArgs(notification));
                }
                break;
            case "delete":
                if (long.TryParse(data, out long statusId))
                {
                    OnDelete?.Invoke(this, new StreamDeleteEventArgs(statusId));
                }
                break;
            case "filters_changed":
                OnFiltersChanged?.Invoke(this, new StreamFiltersChangedEventArgs());
                break;
            case "conversation":
                var conversation = JsonSerializer.Deserialize(data, TryDeserializeContext.Default.Conversation);

                if (conversation is not null)
                {
                    OnConversation?.Invoke(this, new StreamConversationEvenTargs(conversation));
                }
                break;
        }
    }

}