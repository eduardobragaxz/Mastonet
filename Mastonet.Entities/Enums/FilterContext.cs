using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mastonet.Entities.Enums;

[Flags]
[JsonConverter(typeof(FilterContextConverter))]
public enum FilterContext
{
    Home = 1,
    Notifications = 2,
    Public = 4,
    Thread = 8
}

public sealed class FilterContextConverter : JsonConverter<FilterContext>
{
    public override FilterContext Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        FilterContext context = 0;

        IEnumerable<string>? contextStrings = JsonSerializer.Deserialize(ref reader, EntitiesContext.Default.IEnumerableString);

        if (contextStrings is not null)
        {
            foreach (string contextString in contextStrings)
            {
                switch (contextString)
                {
                    case "home":
                        context |= FilterContext.Home;
                        break;
                    case "notifications":
                        context |= FilterContext.Notifications;
                        break;
                    case "public":
                        context |= FilterContext.Public;
                        break;
                    case "thread":
                        context |= FilterContext.Thread;
                        break;
                }
            }
        }

        return context;
    }

    public override void Write(Utf8JsonWriter writer, FilterContext value, JsonSerializerOptions options)
    {
        List<string> contextStrings = [];
        if ((value & FilterContext.Home) == FilterContext.Home)
        {
            contextStrings.Add("home");
        }

        if ((value & FilterContext.Notifications) == FilterContext.Notifications)
        {
            contextStrings.Add("notifications");
        }

        if ((value & FilterContext.Public) == FilterContext.Public)
        {
            contextStrings.Add("public");
        }

        if ((value & FilterContext.Thread) == FilterContext.Thread)
        {
            contextStrings.Add("thread");
        }

        JsonSerializer.Serialize(writer, contextStrings, EntitiesContext.Default.ListString);

    }
}