namespace Web.BlazorServer.Services.Repositories;

public interface IFormCacheService
{
    Task SaveAsync<T>(string key, T data);
    Task<T?> LoadAsync<T>(string key, int ttlMinutes = 30) where T : class;
    Task ClearAsync(string key);
}
