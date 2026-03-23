namespace hotel_management.Domain;

public sealed class Receipt
{
    public string ReceiptId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public decimal NetPrice { get; set; }
    public decimal Discount { get; set; }
}
