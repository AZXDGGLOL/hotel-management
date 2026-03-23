using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Domain;

namespace hotel_management.DAL;

public sealed class EquipmentRepository
{
    public DataTable GetAll()
    {
        const string sql = """
                           SELECT d.DeviceId,
                                  d.DeviceName,
                                  d.Brand,
                                  d.Model,
                                  d.UnitPrice,
                                  d.DevicePicture,
                                  d.DeviceStatus,
                                  d.CategoryId,
                                  c.CategoryName
                           FROM [Devices] d
                           INNER JOIN [Device Category] c ON c.CategoryId = d.CategoryId
                           ORDER BY d.DeviceId
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetCategoryLookup()
    {
        const string sql = """
                           SELECT CategoryId, CategoryName
                           FROM [Device Category]
                           ORDER BY CategoryName
                           """;
        return SqlDataAccess.Query(sql);
    }

    public DataTable GetDeviceLookup()
    {
        const string sql = """
                           SELECT DeviceId, DeviceName + N' (' + CAST(DeviceId AS NVARCHAR(20)) + N')' AS DeviceLabel
                           FROM [Devices]
                           ORDER BY DeviceName
                           """;
        return SqlDataAccess.Query(sql);
    }

    public int Add(Equipment equipment)
    {
        const string sql = """
                           INSERT INTO [Devices] (DeviceName, Brand, Model, UnitPrice, DevicePicture, DeviceStatus, CategoryId)
                           VALUES (@DeviceName, @Brand, @Model, @UnitPrice, @DevicePicture, @DeviceStatus, @CategoryId)
                           """;
        return SqlDataAccess.Execute(sql, BuildInsertParameters(equipment));
    }

    public int Update(Equipment equipment)
    {
        const string sql = """
                           UPDATE [Devices]
                           SET DeviceName = @DeviceName,
                               Brand = @Brand,
                               Model = @Model,
                               UnitPrice = @UnitPrice,
                               DevicePicture = @DevicePicture,
                               DeviceStatus = @DeviceStatus,
                               CategoryId = @CategoryId
                           WHERE DeviceId = @DeviceId
                           """;
        return SqlDataAccess.Execute(sql, BuildUpdateParameters(equipment));
    }

    public int Delete(string deviceId)
    {
        const string sql = "DELETE FROM [Devices] WHERE DeviceId = @DeviceId";
        return SqlDataAccess.Execute(sql, new SqlParameter("@DeviceId", deviceId));
    }

    public int AssignToRoom(string roomId, string deviceId)
    {
        const string sql = """
                           INSERT INTO [Room Devices] (DeviceId, RoomId)
                           VALUES (@DeviceId, @RoomId)
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@DeviceId", deviceId),
            new SqlParameter("@RoomId", roomId));
    }

    public int RemoveFromRoom(string roomId, string deviceId)
    {
        const string sql = """
                           DELETE FROM [Room Devices]
                           WHERE DeviceId = @DeviceId AND RoomId = @RoomId
                           """;
        return SqlDataAccess.Execute(sql,
            new SqlParameter("@DeviceId", deviceId),
            new SqlParameter("@RoomId", roomId));
    }

    public DataTable GetEquipmentByRoom(string roomId)
    {
        const string sql = """
                           SELECT rd.RoomId,
                                  rd.DeviceId,
                                  d.DeviceName,
                                  d.Brand,
                                  d.Model,
                                  d.DeviceStatus
                           FROM [Room Devices] rd
                           INNER JOIN [Devices] d ON d.DeviceId = rd.DeviceId
                           WHERE rd.RoomId = @RoomId
                           ORDER BY d.DeviceName
                           """;
        return SqlDataAccess.Query(sql, new SqlParameter("@RoomId", roomId));
    }

    private static SqlParameter[] BuildInsertParameters(Equipment equipment)
    {
        return
        [
            new SqlParameter("@DeviceName", equipment.DeviceName),
            new SqlParameter("@Brand", (object?)equipment.Brand ?? DBNull.Value),
            new SqlParameter("@Model", (object?)equipment.Model ?? DBNull.Value),
            new SqlParameter("@UnitPrice", equipment.UnitPrice),
            new SqlParameter("@DevicePicture", (object?)equipment.DevicePicture ?? DBNull.Value),
            new SqlParameter("@DeviceStatus", equipment.DeviceStatus),
            new SqlParameter("@CategoryId", equipment.CategoryId)
        ];
    }

    private static SqlParameter[] BuildUpdateParameters(Equipment equipment)
    {
        List<SqlParameter> list = new(BuildInsertParameters(equipment))
        {
            new SqlParameter("@DeviceId", equipment.DeviceId)
        };
        return list.ToArray();
    }
}
