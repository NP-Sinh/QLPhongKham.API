using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace QLPhongKham.API.Services.MemoryCaching
{
    public interface IMemoryCaching
    {
        T? Get<T>(string key);
        Task<T?> GetAsync<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiration = null);
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);
        void Remove(string key);
        Task RemoveAsync(string key);
        Task RemoveByPatternAsync(string pattern);
        bool Exists(string key);
        Task ClearAllAsync();
    }

    public class MemoryCaching : IMemoryCaching
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<MemoryCaching> _logger;
        private readonly ConcurrentDictionary<string, bool> _cacheKeys;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

        public MemoryCaching(IMemoryCache cache, ILogger<MemoryCaching> logger)
        {
            _cache = cache;
            _logger = logger;
            _cacheKeys = new ConcurrentDictionary<string, bool>();
        }

        public T? Get<T>(string key)
        {
            try
            {
                if (_cache.TryGetValue(key, out T? value))
                {
                    _logger.LogDebug("Cache hit: {Key}", key);
                    return value;
                }

                _logger.LogDebug("Cache miss: {Key}", key);
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy cache: {Key}", key);
                return default;
            }
        }

        public Task<T?> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            try
            {
                var cacheExpiration = expiration ?? _defaultExpiration;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(cacheExpiration)
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .RegisterPostEvictionCallback((k, v, r, s) =>
                    {
                        _cacheKeys.TryRemove(k.ToString()!, out _);
                        _logger.LogDebug("Cache bị xóa: {Key}, Lý do: {Reason}", k, r);
                    });

                _cache.Set(key, value, cacheEntryOptions);
                _cacheKeys.TryAdd(key, true);

                _logger.LogDebug("Cache được lưu: {Key}, Hết hạn sau: {Expiration}", key, cacheExpiration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lưu cache: {Key}", key);
            }
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            Set(key, value, expiration);
            return Task.CompletedTask;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            try
            {
                // Kiểm tra cache trước
                var cachedValue = Get<T>(key);
                if (cachedValue != null)
                {
                    return cachedValue;
                }

                // Nếu không có trong cache, gọi factory để lấy data
                _logger.LogDebug("Tạo cache mới cho key: {Key}", key);
                var value = await factory();

                if (value != null)
                {
                    Set(key, value, expiration);
                }

                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong GetOrCreateAsync: {Key}", key);
                // Nếu có lỗi, vẫn cố gắng lấy data từ factory
                return await factory();
            }
        }

        public void Remove(string key)
        {
            try
            {
                _cache.Remove(key);
                _cacheKeys.TryRemove(key, out _);
                _logger.LogDebug("Cache đã xóa: {Key}", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa cache: {Key}", key);
            }
        }

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.CompletedTask;
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            try
            {
                // Chuyển pattern thành regex (ví dụ: "benhnhan:*" -> "^benhnhan:.*")
                var regexPattern = "^" + pattern.Replace("*", ".*").Replace("?", ".") + "$";
                var regex = new System.Text.RegularExpressions.Regex(regexPattern);

                var keysToRemove = _cacheKeys.Keys
                    .Where(k => regex.IsMatch(k))
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    Remove(key);
                }

                _logger.LogDebug("Đã xóa {Count} cache khớp với pattern: {Pattern}", keysToRemove.Count, pattern);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa cache theo pattern: {Pattern}", pattern);
            }

            return Task.CompletedTask;
        }

        public bool Exists(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public Task ClearAllAsync()
        {
            try
            {
                var keys = _cacheKeys.Keys.ToList();
                foreach (var key in keys)
                {
                    Remove(key);
                }
                _logger.LogInformation("Đã xóa tất cả cache: {Count} entries", keys.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa tất cả cache");
            }

            return Task.CompletedTask;
        }
    }
}