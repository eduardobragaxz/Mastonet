using Mastonet.Entities;
using Mastonet.Entities.Enums;
using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using static Mastonet.TimelineWebSocketStreaming;

namespace Mastonet;

public class TimelineWebSocketStreaming(StreamingType type, string? param, string instance, Task<InstanceV2> instanceGetter, string? accessToken, HttpClient client) : TimelineHttpStreaming(type, param, instance, accessToken, client)
{
    private ClientWebSocket? socket;
    private const int receiveChunkSize = 512;

    public TimelineWebSocketStreaming(StreamingType type, string? param, string instance, Task<InstanceV2> instanceGetter, string? accessToken)
        : this(type, param, instance, instanceGetter, accessToken, DefaultHttpClient.Instance) { }

    public override async Task Start()
    {
        InstanceV2 instance = await instanceGetter.ConfigureAwait(false);
        string? url = instance?.Configuration?.Urls?.Streaming;

        if (url is null)
        {
            // websocket disabled, fallback to http streaming
            await base.Start().ConfigureAwait(false);
            return;
        }

        url += $"/api/v1/streaming?access_token={accessToken}";

        url += streamingType switch
        {
            StreamingType.User => "&stream=user",
            StreamingType.Public => "&stream=public",
            StreamingType.PublicLocal => "&stream=public:local",
            StreamingType.Hashtag => $"&stream=hashtag&tag={param}",
            StreamingType.HashtagLocal => $"&stream=hashtag:local&tag={param}",
            StreamingType.List => $"&stream=list&list={param}",
            StreamingType.Direct => "&stream=direct",
            _ => throw new NotImplementedException(),
        };
        socket = new ClientWebSocket();
        await socket.ConnectAsync(new Uri(url), CancellationToken.None).ConfigureAwait(false);

        byte[] buffer = new byte[receiveChunkSize];
        MemoryStream ms = new();
        while (socket is not null)
        {
            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(false);

            ms.Write(buffer, 0, result.Count);

            if (result.EndOfMessage)
            {
                //var messageStr = Encoding.UTF8.GetString(ms.ToArray());

                TimelineMessage? message = JsonSerializer.Deserialize(ms, TimelineMessageContext.Default.TimelineMessage);

                if (message is not null)
                {
                    SendEvent(message.Event, message.Payload);
                }

                ms.Dispose();
                ms = new MemoryStream();
            }
        }
        ms.Dispose();

        this.Stop();
    }

    internal class TimelineMessage
    {
        [JsonPropertyName("event")]
        public string Event { get; set; } = default!;

        [JsonPropertyName("payload")]
        public string Payload { get; set; } = default!;
    }

    public override void Stop()
    {
        if (socket is not null)
        {
            socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            socket.Dispose();
            socket = null;
        }

        base.Stop();
    }
}

[JsonSerializable(typeof(TimelineMessage), GenerationMode = JsonSourceGenerationMode.Metadata)]
internal partial class TimelineMessageContext : JsonSerializerContext
{
}