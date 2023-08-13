using StackExchange.Redis;

namespace Innoloft.Events.Api.Services.Interfaces;

public interface IRedisCacheManager
{
    IDatabase Database { get; }
    Task SetAsync(string key, string value, TimeSpan expiryTimeInDays);
    Task<string> GetAsync(string key);
    Task<List<string>> ScanKeysAsync(string pattern);
    Task DeleteKey(string key);
}