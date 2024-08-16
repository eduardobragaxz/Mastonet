using Mastonet.Entities;
using Mastonet.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mastonet;

public class TimelineHttpStreaming(StreamingType type, string? param, string instance, string? accessToken, HttpClient client) : TimelineStreaming(type, param, accessToken)
{
    private CancellationTokenSource? cts;

    public TimelineHttpStreaming(StreamingType type, string? param, string instance, string? accessToken)
        : this(type, param, instance, accessToken, DefaultHttpClient.Instance) { }

    public override async Task Start()
    {
        string url = "https://" + instance;
        url += streamingType switch
        {
            StreamingType.User => "/api/v1/streaming/user",
            StreamingType.Public => "/api/v1/streaming/public",
            StreamingType.PublicLocal => "/api/v1/streaming/public/local",
            StreamingType.Hashtag => "/api/v1/streaming/hashtag?tag=" + param,
            StreamingType.HashtagLocal => "/api/v1/streaming/hashtag/local?tag=" + param,
            StreamingType.List => "/api/v1/streaming/list?list=" + param,
            StreamingType.Direct => "/api/v1/streaming/direct",
            _ => throw new NotImplementedException(),
        };
        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
        using (cts = new CancellationTokenSource())
        {
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cts.Token);
            var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            string? eventName = null;
            string? data = null;

            while (true)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrEmpty(line) || line.StartsWith(':'))
                {
                    eventName = null;
                    continue;
                }

                if (line.StartsWith("event: "))
                {
                    eventName = line["event: ".Length..].Trim();
                }
                else if (line.StartsWith("data: "))
                {
                    data = line["data: ".Length..];
                    if (eventName != null)
                    {
                        SendEvent(eventName, data);
                    }
                }
            }
        }
    }

    public override void Stop()
    {
        if (cts != null)
        {
            cts.Cancel();
            cts = null;
        }
    }
}
