namespace hotel_management.BLL;

public sealed class ServiceResult
{
    private ServiceResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; }
    public string Message { get; }

    public static ServiceResult Ok(string message) => new(true, message);
    public static ServiceResult Fail(string message) => new(false, message);
}
