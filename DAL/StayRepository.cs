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
                                  m.Name + N' ' + m.LastName AS MemberName
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
                                  StayId + N' - ' + MemberId AS StayLabel
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
                                  rl.PricePerDay
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
                           INSERT INTO [Stay] (StayId, EntryDate, MemberId)
                           VALUES (@StayId, @EntryDate, @MemberId)
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@StayId", stay.StayId),
            new SqlParameter("@EntryDate", stay.EntryDate.Date),
            new SqlParameter("@MemberId", stay.MemberId));
    }

    public int AddStayDetail(StayDetail detail)
    {
        const string sql = """
                           INSERT INTO [Stay List] (StayID, StaySequence, CheckInDate, CheckOutDate, Comment, PaymentStatus, ReceiptId, RoomId)
                           VALUES (@StayID, @StaySequence, @CheckInDate, @CheckOutDate, @Comment, @PaymentStatus, @ReceiptId, @RoomId)
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@StayID", detail.StayId),
            new SqlParameter("@StaySequence", detail.StaySequenceNo),
            new SqlParameter("@CheckInDate", detail.CheckInDate.Date),
            new SqlParameter("@CheckOutDate", detail.CheckOutDate.Date),
            new SqlParameter("@Comment", (object?)detail.Note ?? DBNull.Value),
            new SqlParameter("@PaymentStatus", detail.PaymentStatus),
            new SqlParameter("@ReceiptId", (object?)detail.ReceiptId ?? DBNull.Value),
            new SqlParameter("@RoomId", detail.RoomNumber));
    }

    public int GetNextSequence(string stayId)
    {
        const string sql = """
                           SELECT ISNULL(MAX(StaySequence), 0) + 1
                           FROM [Stay List]
                           WHERE StayID = @StayID
                           """;
        object? value = SqlDataAccess.Scalar(sql, new SqlParameter("@StayID", stayId));
        return Convert.ToInt32(value);
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
}
