namespace hotel_management.Domain;

public sealed class Stay
{
    public string StayId { get; set; } = string.Empty;
    public DateTime EntryDate { get; set; }
    public string MemberId { get; set; } = string.Empty;
}
