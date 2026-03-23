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

public sealed class ServiceResult<T>
{
    private ServiceResult(bool success, string message, T? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public bool Success { get; }
    public string Message { get; }
    public T? Data { get; }

    public static ServiceResult<T> Ok(string message, T? data) => new(true, message, data);
    public static ServiceResult<T> Fail(string message) => new(false, message, default);
}
