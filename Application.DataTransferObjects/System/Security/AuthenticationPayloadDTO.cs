namespace Application.DataTransferObjects.System.Security;

public class AuthenticationPayloadDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}
