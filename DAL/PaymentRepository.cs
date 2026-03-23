using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Domain;

namespace hotel_management.DAL;

public sealed class PaymentRepository
{
    public DataTable GetPendingStayDetails()
    {
        const string sql = """
                           SELECT sl.StayID,
                                  sl.StaySequence,
                                  sl.CheckInDate,
                                  sl.CheckOutDate,
                                  sl.RoomId,
                                  sl.PaymentStatus,
                                  st.MemberId,
                                  m.Name + N' ' + m.LastName AS CustomerName,
                                  m.Address,
                                  m.Phone,
                                  rl.PricePerDay,
                                  CAST(
                                      CASE
                                          WHEN DATEDIFF(DAY, sl.CheckInDate, sl.CheckOutDate) <= 0
                                              THEN rl.PricePerDay
                                          ELSE DATEDIFF(DAY, sl.CheckInDate, sl.CheckOutDate) * rl.PricePerDay
                                      END AS DECIMAL(12, 2)
                                  ) AS GrossAmount
                           FROM [Stay List] sl
                           INNER JOIN [Stay] st ON st.StayId = sl.StayID
                           INNER JOIN [Member] m ON m.MemberId = st.MemberId
                           INNER JOIN [Rooms] r ON r.RoomId = sl.RoomId
                           INNER JOIN [Room Levels] rl ON rl.LevelId = r.LevelId
                           WHERE ISNULL(sl.PaymentStatus, 0) <> 1
                           ORDER BY sl.StayID DESC, sl.StaySequence DESC
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetReceipts()
    {
        const string sql = """
                           SELECT ReceiptID,
                                  [Date],
                                  CustomerName,
                                  Address,
                                  Phone,
                                  NetPrice,
                                  Discount
                           FROM [Receipt]
                           ORDER BY [Date] DESC, ReceiptID DESC
                           """;
        return SqlDataAccess.Query(sql);
    }

    public int ProcessPayment(Receipt receipt, string stayId, int sequenceNo)
    {
        using SqlConnection conn = DbConnectionFactory.CreateConnection();
        conn.Open();
        using SqlTransaction tx = conn.BeginTransaction();

        try
        {
            const string insertReceiptSql = """
                                            INSERT INTO [Receipt] ([Date], CustomerName, Address, Phone, NetPrice, Discount)
                                            VALUES (@Date, @CustomerName, @Address, @Phone, @NetPrice, @Discount);
                                            SELECT CAST(SCOPE_IDENTITY() AS INT);
                                            """;
            using SqlCommand insertCmd = new SqlCommand(insertReceiptSql, conn, tx);
            insertCmd.Parameters.AddWithValue("@Date", receipt.Date);
            insertCmd.Parameters.AddWithValue("@CustomerName", receipt.CustomerName);
            insertCmd.Parameters.AddWithValue("@Address", (object?)receipt.Address ?? DBNull.Value);
            insertCmd.Parameters.AddWithValue("@Phone", (object?)receipt.Phone ?? DBNull.Value);
            insertCmd.Parameters.AddWithValue("@NetPrice", receipt.NetPrice);
            insertCmd.Parameters.AddWithValue("@Discount", receipt.Discount);
            int newReceiptId = Convert.ToInt32(insertCmd.ExecuteScalar());

            const string updateStaySql = """
                                         UPDATE [Stay List]
                                         SET PaymentStatus = @PaymentStatus,
                                             ReceiptId = @ReceiptID
                                         WHERE StayID = @StayID
                                           AND StaySequence = @StaySequence
                                         """;
            using SqlCommand updateCmd = new SqlCommand(updateStaySql, conn, tx);
            updateCmd.Parameters.AddWithValue("@PaymentStatus", 1);
            updateCmd.Parameters.AddWithValue("@ReceiptID", newReceiptId);
            updateCmd.Parameters.AddWithValue("@StayID", stayId);
            updateCmd.Parameters.AddWithValue("@StaySequence", sequenceNo);
            int affected = updateCmd.ExecuteNonQuery();

            tx.Commit();
            return affected;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }
}
