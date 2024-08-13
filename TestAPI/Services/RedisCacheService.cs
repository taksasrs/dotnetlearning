using StackExchange.Redis;

public class RedisCacheService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    public RedisCacheService(string connectionString)
    {
        _redis = ConnectionMultiplexer.Connect(connectionString);
        _database = _redis.GetDatabase();
    }

    public async Task SetStringAsync(string key, string value, TimeSpan expiration)
    {
        await _database.StringSetAsync(key, value, expiration);
    }

    public async Task<string> GetStringAsync(string key)
    { 
        return await _database.StringGetAsync(key);
    }

    public void RemoveKey(string key)
    {
        _database.KeyDelete(key);
    }
}
