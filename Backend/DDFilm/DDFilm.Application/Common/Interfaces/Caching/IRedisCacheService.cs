namespace DDFilm.Application.Common.Interfaces.Caching
{
    public interface IRedisCacheService
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data);
        Task<T?> GetDataAsync<T>(string key);
        Task SetDataAsync<T>(string key, T data);
        Task<bool> KeyExistsAsync(string key);
        Task RemoveDataAsync(string key);
    }
}
