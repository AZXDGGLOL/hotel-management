namespace hotel_management.Domain;

public sealed class StayDetail
{
    public string StayId { get; set; } = string.Empty;
    public int StaySequenceNo { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string? Note { get; set; }
    public string PaymentStatus { get; set; } = "0";
    public string? ReceiptId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
}
