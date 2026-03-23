namespace hotel_management.Domain;

public sealed class Room
{
    public string RoomId { get; set; } = string.Empty;
    public int Floor { get; set; }
    public string LevelId { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
}
