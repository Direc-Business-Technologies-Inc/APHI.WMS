using System.Text;
using Web.BlazorServer.Services.Repositories;

namespace Web.BlazorServer.Services.Implementation;

public class AppErrorLogger : IAppErrorLogger
{
    private readonly string _logsPath;
    private static readonly SemaphoreSlim _writeLock = new(1, 1);

    public AppErrorLogger()
    {
        _logsPath = Path.Combine(AppContext.BaseDirectory, "logs");
        Directory.CreateDirectory(_logsPath);
    }

    public async Task LogAsync(string moduleName, Exception ex)
    {
        var sanitized = SanitizeName(moduleName);
        var timestamp = DateTime.Now.ToString("MM-dd-yyyy-HH-mm");
        var filePath = Path.Combine(_logsPath, $"{sanitized}-{timestamp}.log");
        var content = BuildLogEntry(moduleName, ex);

        await _writeLock.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(filePath, content);
        }
        finally
        {
            _writeLock.Release();
        }
    }

    public IReadOnlyList<FileInfo> GetLogFiles()
    {
        if (!Directory.Exists(_logsPath))
            return [];

        return new DirectoryInfo(_logsPath)
            .GetFiles("*.log")
            .OrderByDescending(f => f.LastWriteTime)
            .ToList();
    }

    public async Task<string> ReadLogFileAsync(string fileName)
    {
        var filePath = Path.Combine(_logsPath, Path.GetFileName(fileName));
        if (!File.Exists(filePath)) return string.Empty;
        return await File.ReadAllTextAsync(filePath);
    }

    public void DeleteLogFile(string fileName)
    {
        var filePath = Path.Combine(_logsPath, Path.GetFileName(fileName));
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public void DeleteAllLogFiles()
    {
        if (!Directory.Exists(_logsPath)) return;
        foreach (var file in Directory.GetFiles(_logsPath, "*.log"))
            File.Delete(file);
    }

    private string BuildLogEntry(string moduleName, Exception ex)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== Error ===");
        sb.AppendLine($"Timestamp : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"Module    : {moduleName}");
        sb.AppendLine($"Type      : {ex.GetType().FullName}");
        sb.AppendLine($"Message   : {ex.Message}");
        sb.AppendLine("Stack     :");
        sb.AppendLine(ex.StackTrace);
        AppendInnerExceptions(sb, ex.InnerException, 1);
        sb.AppendLine("=============");
        sb.AppendLine();
        return sb.ToString();
    }

    private static void AppendInnerExceptions(StringBuilder sb, Exception? ex, int depth)
    {
        if (ex is null) return;
        sb.AppendLine($"Inner ({depth}): {ex.GetType().FullName}: {ex.Message}");
        sb.AppendLine(ex.StackTrace);
        AppendInnerExceptions(sb, ex.InnerException, depth + 1);
    }

    private static string SanitizeName(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        return string.Concat(name.Select(c => invalid.Contains(c) || c == ' ' ? '-' : c));
    }
}
