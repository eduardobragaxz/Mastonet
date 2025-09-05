using Mastonet.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mastonet;

public abstract partial class BaseHttpClient
{
    public HttpClient Client { get; private init; }

    public string AccessToken { get; protected set; } = string.Empty;

    #region Instance 
    private string instance = string.Empty;
    public string Instance
    {
        get
        {
            return instance;
        }
        protected set
        {
            instance = CheckInstance(value);
        }
    }

    private static string CheckInstance(string instance)
    {
        if (string.IsNullOrWhiteSpace(instance))
        {
            throw new ArgumentNullException(nameof(instance));
        }

        if (instance.StartsWith("https://"))
        {
            instance = instance["https://".Length..];
        }

        List<string> notSupportedList = ["gab.", "truthsocial."];
        string lowered = instance.ToLowerInvariant();
        if (notSupportedList.Any(n => lowered.Contains(n)))
        {
            throw new NotSupportedException();
        }

        return instance;
    }

    #endregion

    protected BaseHttpClient()
    {
        this.Client = DefaultHttpClient.Instance;
    }
    protected BaseHttpClient(HttpClient client, string accessToken)
    {
        if (accessToken != "")
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            AccessToken = accessToken;
        }

        this.Client = client;
    }

    #region Http helpers

    protected abstract void OnResponseReceived(HttpResponseMessage response);

    protected async Task<Stream> Delete(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        string url = $"https://{this.Instance}{route}";
        if (data is not null)
        {
            string querystring = $"?{String.Join("&", data.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
            url += querystring;
        }

        HttpResponseMessage response = await Client.DeleteAsync(url).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }


    protected async Task<Stream> Get(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        string url = $"https://{this.Instance}{route}";
        if (data is not null)
        {
            string querystring = $"?{String.Join("&", data.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
            url += querystring;
        }

        HttpResponseMessage response = await Client.GetAsync(url).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    protected async Task<T> Get<T>(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
        where T : class
    {
        using Stream content = await Get(route, data).ConfigureAwait(false);
        return TryDeserialize<T>(content);
    }

    protected async Task<T> GetValue<T>(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
        where T : struct
    {
        using Stream content = await Get(route, data).ConfigureAwait(false);
        return TryDeserialize<T>(content);
    }

    //private const string ID_FINDER_PATTERN = "_id=([0-9]+)";
    [GeneratedRegex("_id=([0-9]+)", RegexOptions.None, 100)]
    private static partial Regex IdFinder();

    protected async Task<MastodonList<T>> GetMastodonList<T>(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        string url = $"https://{this.Instance}{route}";
        if (data is not null)
        {
            string querystring = $"?{String.Join("&", data.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";
            url += querystring;
        }

        using HttpResponseMessage response = await Client.GetAsync(url).ConfigureAwait(false);
        OnResponseReceived(response);
        using Stream content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        MastodonList<T> result = TryDeserialize<MastodonList<T>>(content);
        // Read `Link` header
        if (response.Headers.TryGetValues("Link", out IEnumerable<string>? linkHeader))
        {
            ReadOnlySpan<string> links = linkHeader.Single().Split(',');
            foreach (string link in links)
            {
                if (link.Contains("rel=\"next\""))
                {
                    result.NextPageMaxId = IdFinder().Match(link).Groups[1].Value;
                }

                if (link.Contains("rel=\"prev\""))
                {
                    if (link.Contains("since_id"))
                    {
                        result.PreviousPageSinceId = IdFinder().Match(link).Groups[1].Value;
                    }
                    if (link.Contains("min_id"))
                    {
                        result.PreviousPageMinId = IdFinder().Match(link).Groups[1].Value;
                    }
                }
            }
        }

        return result;
    }

    protected async Task<Stream> Post(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        string url = $"https://{this.Instance}{route}";

        HttpResponseMessage response = await Client.PostAsync(url, new FormUrlEncodedContent(data ?? [])).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    protected async Task<T> Post<T>(string route, IEnumerable<KeyValuePair<string, string>>? data = null, IEnumerable<MediaDefinition>? media = null)
        where T : class
    {
        Stream content = media is not null && media.Any() ? await PostMedia(route, data, media).ConfigureAwait(false) : await Post(route, data).ConfigureAwait(false);
        return TryDeserialize<T>(content);
    }

    protected async Task<Stream> PostMedia(string route, IEnumerable<KeyValuePair<string, string>>? data = null, IEnumerable<MediaDefinition>? media = null)
    {
        string url = $"https://{this.Instance}{route}";

        MultipartFormDataContent content = [];

        if (media is not null)
        {
            foreach (MediaDefinition m in media)
            {
                content.Add(new StreamContent(m.Media), m.ParamName!, m.FileName);
            }
        }

        if (data is not null)
        {
            foreach (KeyValuePair<string, string> pair in data)
            {
                content.Add(new StringContent(pair.Value), pair.Key);
            }
        }

        HttpResponseMessage response = await Client.PostAsync(url, content).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    protected async Task<Stream> Put(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        string url = $"https://{this.Instance}{route}";

        HttpResponseMessage response = await Client.PutAsync(url, new FormUrlEncodedContent(data ?? [])).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    protected async Task<T> Put<T>(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        return TryDeserialize<T>(await Put(route, data).ConfigureAwait(false));
    }

    protected async Task<Stream> Patch(string route, IEnumerable<KeyValuePair<string, string>>? data = null)
    {
        string url = $"https://{this.Instance}{route}";

        HttpResponseMessage response = await Client.PatchAsync(url, new FormUrlEncodedContent(data ?? [])).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    protected async Task<T> Patch<T>(string route, IEnumerable<KeyValuePair<string, string>>? data = null, IEnumerable<MediaDefinition>? media = null)
        where T : class
    {
        Stream content = media is not null && media.Any() ? await PatchMedia(route, data, media).ConfigureAwait(false) : await Patch(route, data).ConfigureAwait(false);
        return TryDeserialize<T>(content);
    }

    protected async Task<Stream> PatchMedia(string route, IEnumerable<KeyValuePair<string, string>>? data = null, IEnumerable<MediaDefinition>? media = null)
    {
        string url = $"https://{this.Instance}{route}";

        MultipartFormDataContent content = [];

        if (media is not null)
        {
            foreach (MediaDefinition m in media)
            {
                content.Add(new StreamContent(m.Media), m.ParamName!, m.FileName);
            }
        }

        if (data is not null)
        {
            foreach (KeyValuePair<string, string> pair in data)
            {
                content.Add(new StringContent(pair.Value), pair.Key);
            }
        }

        HttpResponseMessage response = await Client.PatchAsync(url, content).ConfigureAwait(false);
        OnResponseReceived(response);
        return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    private static T TryDeserialize<T>(Stream json)
    {
        return (T)JsonSerializer.Deserialize(json, typeof(T), TryDeserializeContext.Default)!;
    }
    protected static string AddQueryStringParam(string queryParams, string queryStringParam, string? value)
    {
        // Empty parm? Exit
        if (string.IsNullOrEmpty(value))
        {
            return queryParams;
        }

        // Figure up delimiter and concat
        string concatChar = GetQueryStringConcatChar(queryParams);
        queryParams += $"{concatChar}{queryStringParam}={value}";
        return queryParams;
    }

    protected static string GetQueryStringConcatChar(string queryParams)
    {
        return !string.IsNullOrEmpty(queryParams) ? "&" : "?";
    }

    #endregion
}