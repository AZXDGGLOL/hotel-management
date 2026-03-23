using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Domain;

namespace hotel_management.DAL;

public sealed class RoomRepository
{
    public DataTable GetAll()
    {
        const string sql = """
                           SELECT r.RoomId,
                                  r.Floor,
                                  r.LevelId,
                                  rl.LevelName,
                                  rl.PricePerDay,
                                  r.CategoryId,
                                  rc.CategoryName
                           FROM [Rooms] r
                           INNER JOIN [Room Levels] rl ON rl.LevelId = r.LevelId
                           INNER JOIN [Room Categories] rc ON rc.CateGoryId = r.CategoryId
                           ORDER BY r.RoomId
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetRoomLookup()
    {
        const string sql = """
                           SELECT RoomId,
                                  RoomId + N' (Floor ' + CAST(Floor AS NVARCHAR(10)) + N')' AS RoomLabel
                           FROM [Rooms]
                           ORDER BY RoomId
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetLevelLookup()
    {
        const string sql = """
                           SELECT LevelId, LevelName
                           FROM [Room Levels]
                           ORDER BY LevelName
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetCategoryLookup()
    {
        const string sql = """
                           SELECT CateGoryId AS CategoryId, CategoryName
                           FROM [Room Categories]
                           ORDER BY CategoryName
                           """;
        return SqlDataAccess.Query(sql);
    }

    public int Add(Room room)
    {
        const string sql = """
                           INSERT INTO [Rooms] (RoomId, Floor, LevelId, CategoryId)
                           VALUES (@RoomId, @Floor, @LevelId, @CategoryId)
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@RoomId", room.RoomId),
            new SqlParameter("@Floor", room.Floor),
            new SqlParameter("@LevelId", room.LevelId),
            new SqlParameter("@CategoryId", room.CategoryId));
    }

    public int Update(Room room)
    {
        const string sql = """
                           UPDATE [Rooms]
                           SET Floor = @Floor,
                               LevelId = @LevelId,
                               CategoryId = @CategoryId
                           WHERE RoomId = @RoomId
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@RoomId", room.RoomId),
            new SqlParameter("@Floor", room.Floor),
            new SqlParameter("@LevelId", room.LevelId),
            new SqlParameter("@CategoryId", room.CategoryId));
    }

    public int Delete(string roomId)
    {
        const string sql = "DELETE FROM [Rooms] WHERE RoomId = @RoomId";
        return SqlDataAccess.Execute(sql, new SqlParameter("@RoomId", roomId));
    }
}
