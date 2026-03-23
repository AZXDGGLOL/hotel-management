using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Domain;

namespace hotel_management.DAL;

public sealed class StayRepository
{
    public DataTable GetStays()
    {
        const string sql = """
                           SELECT s.StayId,
                                  s.EntryDate,
                                  s.MemberId,
                                  m.Name + N' ' + m.LastName AS MemberName,
                                  m.Address,
                                  m.Phone
                           FROM [Stay] s
                           INNER JOIN [Member] m ON m.MemberId = s.MemberId
                           ORDER BY s.EntryDate DESC, s.StayId DESC
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetStayLookup()
    {
        const string sql = """
                           SELECT StayId,
                                  CAST(StayId AS NVARCHAR(20)) + N' - ' + CAST(MemberId AS NVARCHAR(20)) AS StayLabel
                           FROM [Stay]
                           ORDER BY EntryDate DESC
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetStayDetails(string stayId = "")
    {
        const string sql = """
                           SELECT sl.StayID,
                                  sl.StaySequence,
                                  sl.CheckInDate,
                                  sl.CheckOutDate,
                                  sl.Comment,
                                  sl.PaymentStatus,
                                  sl.ReceiptId,
                                  sl.RoomId,
                                  rl.PricePerDay,
                                  CAST(
                                      CASE
                                          WHEN DATEDIFF(DAY, sl.CheckInDate, sl.CheckOutDate) <= 0
                                              THEN rl.PricePerDay
                                          ELSE DATEDIFF(DAY, sl.CheckInDate, sl.CheckOutDate) * rl.PricePerDay
                                      END AS DECIMAL(12, 2)
                                  ) AS GrossAmount
                           FROM [Stay List] sl
                           INNER JOIN [Rooms] r ON r.RoomId = sl.RoomId
                           INNER JOIN [Room Levels] rl ON rl.LevelId = r.LevelId
                           WHERE @StayId = N'' OR sl.StayID = @StayId
                           ORDER BY sl.StayID DESC, sl.StaySequence DESC
                           """;
        return SqlDataAccess.Query(sql, new SqlParameter("@StayId", stayId.Trim()));
    }

    public int AddStay(Stay stay)
    {
        const string sql = """
                           INSERT INTO [Stay] (EntryDate, MemberId)
                           VALUES (@EntryDate, @MemberId);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);
                           """;
        object? value = SqlDataAccess.Scalar(sql,
            new SqlParameter("@EntryDate", stay.EntryDate.Date),
            new SqlParameter("@MemberId", stay.MemberId));
        return Convert.ToInt32(value);
    }

    public int AddStayDetail(StayDetail detail)
    {
        const string sql = """
                           INSERT INTO [Stay List] (StayID, CheckInDate, CheckOutDate, Comment, PaymentStatus, ReceiptId, RoomId)
                           VALUES (@StayID, @CheckInDate, @CheckOutDate, @Comment, @PaymentStatus, @ReceiptId, @RoomId)
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@StayID", detail.StayId),
            new SqlParameter("@CheckInDate", detail.CheckInDate.Date),
            new SqlParameter("@CheckOutDate", detail.CheckOutDate.Date),
            new SqlParameter("@Comment", (object?)detail.Note ?? DBNull.Value),
            new SqlParameter("@PaymentStatus", detail.PaymentStatus),
            new SqlParameter("@ReceiptId", (object?)detail.ReceiptId ?? DBNull.Value),
            new SqlParameter("@RoomId", detail.RoomNumber));
    }

    public bool IsRoomAvailable(string roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        // Overlap condition for [start, end) windows.
        const string sql = """
                           SELECT COUNT(1)
                           FROM [Stay List]
                           WHERE RoomId = @RoomId
                             AND @CheckInDate < CheckOutDate
                             AND @CheckOutDate > CheckInDate
                           """;
        object? value = SqlDataAccess.Scalar(sql,
            new SqlParameter("@RoomId", roomId),
            new SqlParameter("@CheckInDate", checkInDate.Date),
            new SqlParameter("@CheckOutDate", checkOutDate.Date));
        return Convert.ToInt32(value) == 0;
    }

    public int DeleteStayDetail(string stayId, int staySequence)
    {
        const string sql = """
                           DELETE FROM [Stay List]
                           WHERE StayId = @StayId
                             AND StaySequence = @StaySequence
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@StayId", stayId),
            new SqlParameter("@StaySequence", staySequence));
    }
}
