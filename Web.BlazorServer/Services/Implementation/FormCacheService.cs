using System.Text.Json;
using Microsoft.JSInterop;
using Web.BlazorServer.Services.Repositories;

namespace Web.BlazorServer.Services.Implementation;

public class FormCacheService(IJSRuntime jsRuntime) : IFormCacheService
{
    private sealed class CacheEnvelope<T>
    {
        public T? Data { get; set; }
        public DateTime SavedAt { get; set; }
    }

    public async Task SaveAsync<T>(string key, T data)
    {
        try
        {
            var envelope = new CacheEnvelope<T> { Data = data, SavedAt = DateTime.UtcNow };
            var json = JsonSerializer.Serialize(envelope);
            await jsRuntime.InvokeVoidAsync("window.localStorage.setItem", key, json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[FormCacheService] SaveAsync failed for key '{key}': {ex.Message}");
        }
    }

    public async Task<T?> LoadAsync<T>(string key, int ttlMinutes = 30) where T : class
    {
        try
        {
            var json = await jsRuntime.InvokeAsync<string?>("window.localStorage.getItem", key);
            if (string.IsNullOrEmpty(json))
                return null;

            var envelope = JsonSerializer.Deserialize<CacheEnvelope<T>>(json);
            if (envelope?.Data is null)
                return null;

            if ((DateTime.UtcNow - envelope.SavedAt).TotalMinutes > ttlMinutes)
            {
                await jsRuntime.InvokeVoidAsync("window.localStorage.removeItem", key);
                return null;
            }

            return envelope.Data;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[FormCacheService] LoadAsync failed for key '{key}': {ex.Message}");
            return null;
        }
    }

    public async Task ClearAsync(string key)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("window.localStorage.removeItem", key);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[FormCacheService] ClearAsync failed for key '{key}': {ex.Message}");
        }
    }
}
