namespace hotel_management.Domain;

public sealed class Equipment
{
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public decimal UnitPrice { get; set; }
    public string? DevicePicture { get; set; }
    public int DeviceStatus { get; set; }
    public string CategoryId { get; set; } = string.Empty;
}
