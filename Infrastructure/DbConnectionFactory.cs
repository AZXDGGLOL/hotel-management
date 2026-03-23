using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

internal static class DbConnectionFactory
{
    public static SqlConnection CreateConnection()
    {
        return HotelDb.CreateConnection();
    }
}
