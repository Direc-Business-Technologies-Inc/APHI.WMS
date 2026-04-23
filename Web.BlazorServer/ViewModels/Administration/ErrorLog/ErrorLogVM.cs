namespace Web.BlazorServer.ViewModels.Administration.ErrorLog;

public class ErrorLogVM
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime LastModified { get; set; }
    public string FileSizeDisplay => FileSize < 1024
        ? $"{FileSize} B"
        : $"{FileSize / 1024.0:F1} KB";
}
