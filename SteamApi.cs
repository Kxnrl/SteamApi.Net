using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using Kxnrl.SteamApi.Responses;

namespace Kxnrl.SteamApi;

public interface ISteamApi
{
    /// <summary>
    ///     Api Interface Name
    /// </summary>
    /// <returns></returns>
    string GetName();
}

internal abstract class SteamApi : ISteamApi
{
    private readonly string _apiKey;
    private readonly IRest  _defaultRest;
    private readonly IRest  _globalRest;

    protected SteamApi(string apiKey)
    {
        _apiKey = apiKey;

        _defaultRest = new Rest("https://api.steamchina.com",   TimeSpan.FromSeconds(30));
        _globalRest  = new Rest("https://api.steampowered.com", TimeSpan.FromMinutes(1));
    }

    public abstract string GetName();

    protected abstract bool RequiredAuthorized();

    private string BuildAuthorized()
        => RequiredAuthorized() ? $"key={_apiKey}&" : "";

    protected async Task<T> Get<T>(string endpoint, string param, bool defaultRest = true) where T : class
    {
        using var http = defaultRest ? _defaultRest.CreateClient() : _globalRest.CreateClient();

        var url      = $"{GetName()}/{endpoint}/?{BuildAuthorized()}{param}";
        var response = await http.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<SteamApiResponse<T>>()
                     ?? throw new InvalidDataException("Invalid upstream data.");

        return result.Response;
    }

    protected async Task<T> Get<T>(string endpoint, string param, string responseKey, bool defaultRest = true) where T : class
    {
        using var http = defaultRest ? _defaultRest.CreateClient() : _globalRest.CreateClient();

        var url      = $"{GetName()}/{endpoint}/?{BuildAuthorized()}{param}";
        var response = await http.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, T>>()
                     ?? throw new InvalidDataException("Invalid upstream data.");

        var body = result.GetValueOrDefault(responseKey) ?? throw new InvalidDataException("Invalid upstream data <name>.");

        return body;
    }

    protected async Task<T> GetRawResponse<T>(string endpoint, string param, bool defaultRest = true) where T : class
    {
        using var http = defaultRest ? _defaultRest.CreateClient() : _globalRest.CreateClient();

        var url      = $"{GetName()}/{endpoint}/?{BuildAuthorized()}{param}";
        var response = await http.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<T>()
                     ?? throw new InvalidDataException("Invalid upstream data.");

        return result;
    }
}

public static class SteamApiFactory
{
    // ReSharper disable MemberCanBePrivate.Global

    /// <summary>
    ///     Get SteamApi Instance
    /// </summary>
    /// <typeparam name="T">Api Interface</typeparam>
    /// <param name="apiKey">Api Key</param>
    /// <exception cref="NotSupportedException"></exception>
    public static T GetSteamApi<T>(string apiKey) where T : ISteamApi
    {
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes();

        var impl = types.SingleOrDefault(x => typeof(T).IsAssignableFrom(x) && x is { IsInterface: false, IsClass: true })
                   ?? throw new NotSupportedException("Invalid Api Interface");

        if (Activator.CreateInstance(impl, apiKey) is not T instance)
        {
            throw new NotSupportedException("Invalid Api Interface");
        }

        return instance;
    }

    /// <summary>
    ///     Get SteamApi Instance (Api Key from Environment)
    /// </summary>
    /// <typeparam name="T">Api Interface</typeparam>
    /// <exception cref="NotSupportedException"></exception>
    public static T GetSteamApi<T>() where T : ISteamApi
    {
        if (Environment.GetEnvironmentVariable("STEAM_API_KEY", EnvironmentVariableTarget.Machine) is not { } apiKey)
        {
            throw new InvalidOperationException("Missing Environment 'STEAM_API_KEY'");
        }

        return GetSteamApi<T>(apiKey);
    }

    // ReSharper restore MemberCanBePrivate.Global
}
