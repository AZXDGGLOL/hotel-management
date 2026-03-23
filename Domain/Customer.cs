namespace hotel_management.Domain;

public sealed class Customer
{
    public string MemberId { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Nationality { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}
