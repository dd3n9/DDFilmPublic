using DDFilm.Application.Common.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace DDFilm.Infrastructure.Common.Services.Caching
{
    internal sealed class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache? _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T? GetData<T>(string key)
        {
            var data = _cache?.GetString(key);

            if(data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            _cache?.SetString(key, JsonSerializer.Serialize(data), options);

        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            var data = await _cache?.GetStringAsync(key);

            if (data is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetDataAsync<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await _cache?.SetStringAsync(key, JsonSerializer.Serialize(data), options);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            var data = await _cache?.GetStringAsync(key);
            return data != null;
        }

        public async Task RemoveDataAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
