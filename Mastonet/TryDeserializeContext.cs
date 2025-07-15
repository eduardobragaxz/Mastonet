using Mastonet.Entities;
using Mastonet.Entities.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Mastonet;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(Status))]
[JsonSerializable(typeof(Quote))]
[JsonSerializable(typeof(QuoteState))]
[JsonSerializable(typeof(Account))]
[JsonSerializable(typeof(Attachment))]
[JsonSerializable(typeof(Card))]
[JsonSerializable(typeof(CardAuthor))]
[JsonSerializable(typeof(Poll))]
[JsonSerializable(typeof(Tag))]
[JsonSerializable(typeof(IEnumerable<Tag>))]
[JsonSerializable(typeof(IEnumerable<Emoji>))]
[JsonSerializable(typeof(MastodonList<Status>))]
[JsonSerializable(typeof(Context))]
[JsonSerializable(typeof(List))]
[JsonSerializable(typeof(MastodonList<Card>))]
[JsonSerializable(typeof(IEnumerable<List>))]
[JsonSerializable(typeof(Conversation))]
[JsonSerializable(typeof(Filter))]
[JsonSerializable(typeof(InstanceV2))]
[JsonSerializable(typeof(List<Account>))]
[JsonSerializable(typeof(Marker))]
[JsonSerializable(typeof(Notification))]
[JsonSerializable(typeof(NotificationType))]
[JsonSerializable(typeof(ScheduledStatus))]
[JsonSerializable(typeof(SearchResults))]
[JsonSerializable(typeof(AppRegistration))]
[JsonSerializable(typeof(Auth))]
[JsonSerializable(typeof(Report))]
[JsonSerializable(typeof(FeaturedTag))]
[JsonSerializable(typeof(Relationship))]
[JsonSerializable(typeof(MastodonList<Account>))]
[JsonSerializable(typeof(MastodonList<string>))]
[JsonSerializable(typeof(MastodonList<Tag>))]
[JsonSerializable(typeof(MastodonList<AdminAccount>))]
[JsonSerializable(typeof(MastodonList<Notification>))]
[JsonSerializable(typeof(MastodonList<Report>))]
[JsonSerializable(typeof(MastodonList<Conversation>))]
[JsonSerializable(typeof(IEnumerable<Relationship>))]
[JsonSerializable(typeof(IEnumerable<Account>))]
[JsonSerializable(typeof(IEnumerable<Activity>))]
[JsonSerializable(typeof(IEnumerable<Announcement>))]
[JsonSerializable(typeof(IEnumerable<FeaturedTag>))]
[JsonSerializable(typeof(IEnumerable<Filter>))]
[JsonSerializable(typeof(IEnumerable<ScheduledStatus>))]
[JsonSerializable(typeof(IEnumerable<string>))]
[JsonSerializable(typeof(ImmutableArray<Filter>))]
internal partial class TryDeserializeContext : JsonSerializerContext
{
}

[JsonSerializable(typeof(Error), GenerationMode = JsonSourceGenerationMode.Metadata)]
internal partial class ErrorContext : JsonSerializerContext
{
}