using Innoloft.Events.Api.Services.Interfaces;
using StackExchange.Redis;

namespace Innoloft.Events.Api.Services.Providers;

public class RedisCacheManager : IRedisCacheManager
{


    private readonly IServer _server;

    public RedisCacheManager(IDatabase database, IServer server)
    {
        Database = database;
        _server = server;
    }

    public IDatabase Database { get; }


    public async Task SetAsync(string key, string value, TimeSpan expiryTimeInDays)
    {
        await Database.StringSetAsync(key, value, expiryTimeInDays);
    }

    public async Task<string> GetAsync(string key)
    {
        var value = await Database.StringGetAsync(key);

        return value;
    }

    public async Task DeleteKey(string key)
    {
      await  Database.KeyDeleteAsync(key);
    }


    public async Task<List<string>> ScanKeysAsync(string pattern)
    {
        await Task.Delay(0);
        var keys = new List<string>();
        if (string.IsNullOrEmpty(pattern))
        {
            return keys;
        }

        foreach (var key in _server.Keys(pattern: $"*{pattern}*"))
        {
            keys.Add(key);
        }

        return keys;
    }
}