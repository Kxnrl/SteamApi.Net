using System;
using System.Net;
using System.Net.Http;

namespace Kxnrl.SteamApi;

internal interface IRest
{
    HttpClient CreateClient();
}

internal class Rest : IRest
{
    private readonly SocketsHttpHandler _httpHandler;
    private readonly Uri                _baseUri;
    private readonly TimeSpan           _timeout;

    public Rest(string url) : this(url, TimeSpan.FromMinutes(1))
    {
    }

    public Rest(string url, TimeSpan timeout)
    {
        _httpHandler = new SocketsHttpHandler
        {
            AllowAutoRedirect              = true,
            AutomaticDecompression         = DecompressionMethods.All,
            EnableMultipleHttp2Connections = true,
            ConnectTimeout                 = TimeSpan.FromSeconds(10),
        };

        _baseUri = new Uri(url);

        _timeout = timeout;
    }

    public HttpClient CreateClient()
    {
        var client = new HttpClient(_httpHandler, false)
        {
            BaseAddress           = _baseUri,
            Timeout               = _timeout,
            DefaultRequestVersion = HttpVersion.Version20,
            DefaultVersionPolicy  = HttpVersionPolicy.RequestVersionOrLower,
        };

        client.DefaultRequestHeaders.Add("User-Agent", "Kxnrl.SteamApi.Client");

        return client;
    }
}
