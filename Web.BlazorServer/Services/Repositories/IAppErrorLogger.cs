namespace Web.BlazorServer.Services.Repositories;

public interface IAppErrorLogger
{
    Task LogAsync(string moduleName, Exception ex);
    IReadOnlyList<FileInfo> GetLogFiles();
    Task<string> ReadLogFileAsync(string fileName);
    void DeleteLogFile(string fileName);
    void DeleteAllLogFiles();
}
