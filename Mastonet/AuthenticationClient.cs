using Mastonet.Entities;
using Mastonet.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mastonet;

public class AuthenticationClient : BaseHttpClient, IAuthenticationClient
{
    public AppRegistration? AppRegistration { get; set; }

    public AuthenticationClient(string instance) : this(instance, DefaultHttpClient.Instance) { }
    public AuthenticationClient(AppRegistration app) : this(app, DefaultHttpClient.Instance) { }

    public AuthenticationClient(string instance, HttpClient client) : base(client, "")
    {
        this.Instance = instance;
    }

    public AuthenticationClient(AppRegistration app, HttpClient client) : base(client, "")
    {
        this.Instance = app.Instance;
        this.AppRegistration = app;
    }

    #region Apps


    public Task<AppRegistration> CreateApp(string appName, string? website = null, string? redirectUri = null, params GranularScope[] scope)
    {
        return CreateApp(appName, website, redirectUri, scope.AsEnumerable());
    }

    public async Task<AppRegistration> CreateApp(string appName, string? website = null, string? redirectUri = null, IEnumerable<GranularScope>? scope = null)
    {
        string scopeString = GetScopeParam(scope);
        List<KeyValuePair<string, string>> data = [
            new("client_name", appName),
            new("scopes", scopeString),
            new("redirect_uris", redirectUri?? "urn:ietf:wg:oauth:2.0:oob")
        ];

        if (website is not null)
        {
            data.Add(new KeyValuePair<string, string>("website", website));
        }

        AppRegistration appRegistration = await Post<AppRegistration>("/api/v1/apps", data).ConfigureAwait(false);

        appRegistration.Instance = Instance;
        appRegistration.Scope = scopeString;
        this.AppRegistration = appRegistration;

        return appRegistration;
    }

    #endregion

    #region Auth

    public Task<Auth> ConnectWithPassword(string email, string password)
    {
        if (AppRegistration == null)
        {
            throw new InvalidOperationException("The app must be registered before you can connect");
        }

        List<KeyValuePair<string, string>> data =
        [
            new("client_id", AppRegistration.ClientId),
            new("client_secret", AppRegistration.ClientSecret),
            new("grant_type", "password"),
            new("username", email),
            new("password", password),
            new("scope", AppRegistration.Scope),
        ];

        return Post<Auth>("/oauth/token", data);
    }

    public Task<Auth> ConnectWithCode(string code, string? redirect_uri = null)
    {
        if (AppRegistration == null)
        {
            throw new InvalidOperationException("The app must be registered before you can connect");
        }

        List<KeyValuePair<string, string>> data =
        [
            new("client_id", AppRegistration.ClientId),
            new("client_secret", AppRegistration.ClientSecret),
            new("grant_type", "authorization_code"),
            new("redirect_uri", redirect_uri ?? "urn:ietf:wg:oauth:2.0:oob"),
            new("code", code),
        ];

        return Post<Auth>("/oauth/token", data);
    }

    public string OAuthUrl(string? redirectUri = null)
    {
        if (AppRegistration == null)
        {
            throw new InvalidOperationException("The app must be registered before you can connect");
        }

        if (redirectUri is not null)
        {
            redirectUri = WebUtility.UrlEncode(WebUtility.UrlDecode(redirectUri));
        }
        else
        {
            redirectUri = "urn:ietf:wg:oauth:2.0:oob";
        }

        return $"https://{this.Instance}/oauth/authorize?response_type=code&client_id={AppRegistration.ClientId}&scope={AppRegistration.Scope.Replace(" ", "%20")}&redirect_uri={redirectUri ?? "urn:ietf:wg:oauth:2.0:oob"}";
    }

    /// <summary>
    /// Revoke an access token to make it no longer valid for use.
    /// </summary>
    /// <param name="token">The previously obtained token, to be invalidated.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public Task Revoke(string token)
    {
        if (AppRegistration == null)
        {
            throw new InvalidOperationException("You need to revoke a token with the app CclientId and ClientSecret used to obtain the Token");
        }

        List<KeyValuePair<string, string>> data =
        [
            new("client_id", AppRegistration.ClientId),
            new("client_secret", AppRegistration.ClientSecret),
            new("token", token),
        ];

        return Post<Auth>("/oauth/revoke", data);
    }

    private static string GetScopeParam(IEnumerable<GranularScope>? scopes)
    {
        if (scopes == null)
        {
            return "";
        }

        return String.Join(" ", scopes.Select(s => $"{s}".ToLowerInvariant().Replace("__", ":")));
    }

    #endregion

    protected override void OnResponseReceived(HttpResponseMessage response)
    {
    }
}
