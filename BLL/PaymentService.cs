using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;
using hotel_management.Domain;

namespace hotel_management.BLL;

public sealed class PaymentService
{
    private readonly PaymentRepository _paymentRepository = new();

    public DataTable GetPendingStayDetails() => _paymentRepository.GetPendingStayDetails();
    public DataTable GetReceipts() => _paymentRepository.GetReceipts();

    public ServiceResult ProcessPayment(Receipt receipt, string stayId, int staySequenceNo, decimal grossAmount)
    {
        if (string.IsNullOrWhiteSpace(stayId) ||
            staySequenceNo <= 0 ||
            string.IsNullOrWhiteSpace(receipt.ReceiptId) ||
            string.IsNullOrWhiteSpace(receipt.CustomerName))
        {
            return ServiceResult.Fail("Receipt ID, customer, and selected stay detail are required.");
        }

        if (receipt.Discount < 0)
        {
            return ServiceResult.Fail("Discount must be >= 0.");
        }

        decimal netPrice = grossAmount - receipt.Discount;
        if (netPrice < 0)
        {
            return ServiceResult.Fail("Discount cannot be greater than gross amount.");
        }

        receipt.NetPrice = netPrice;

        try
        {
            int affected = _paymentRepository.ProcessPayment(receipt, stayId, staySequenceNo);
            return affected > 0
                ? ServiceResult.Ok("Payment completed and receipt saved.")
                : ServiceResult.Fail("Stay detail not found.");
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            return ServiceResult.Fail("Receipt ID already exists.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid stay detail or receipt relationship.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while processing payment.");
        }
    }
}
