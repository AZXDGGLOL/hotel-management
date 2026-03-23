using System.Data;
using Microsoft.Data.SqlClient;

namespace hotel_management.Data;

internal static class HotelDb
{
    private const string DefaultConnectionString =
        @"Data Source=.\SQLEXPRESS;Initial Catalog=HotelDB;Integrated Security=True;TrustServerCertificate=True;";

    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(GetConnectionString());
    }

    public static DataTable Query(string sql, params SqlParameter[] parameters)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = BuildCommand(conn, sql, parameters);
        using SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        DataTable table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static int Execute(string sql, params SqlParameter[] parameters)
    {
        using SqlConnection conn = CreateConnection();
        using SqlCommand cmd = BuildCommand(conn, sql, parameters);
        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    private static SqlCommand BuildCommand(SqlConnection conn, string sql, SqlParameter[]? parameters)
    {
        SqlCommand cmd = new SqlCommand(sql, conn);
        if (parameters is { Length: > 0 })
        {
            cmd.Parameters.AddRange(parameters);
        }

        return cmd;
    }

    private static string GetConnectionString()
    {
        return Environment.GetEnvironmentVariable("HOTEL_DB_CONNECTION") ?? DefaultConnectionString;
    }
}
