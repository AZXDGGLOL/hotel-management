using System.Data;
using Microsoft.Data.SqlClient;

namespace hotel_management.DAL;

internal static class SqlDataAccess
{
    public static DataTable Query(string sql, params SqlParameter[] parameters)
    {
        using SqlConnection conn = DbConnectionFactory.CreateConnection();
        using SqlCommand cmd = new SqlCommand(sql, conn);
        if (parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }

        using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static int Execute(string sql, params SqlParameter[] parameters)
    {
        using SqlConnection conn = DbConnectionFactory.CreateConnection();
        using SqlCommand cmd = new SqlCommand(sql, conn);
        if (parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }

        conn.Open();
        return cmd.ExecuteNonQuery();
    }

    public static object? Scalar(string sql, params SqlParameter[] parameters)
    {
        using SqlConnection conn = DbConnectionFactory.CreateConnection();
        using SqlCommand cmd = new SqlCommand(sql, conn);
        if (parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }

        conn.Open();
        return cmd.ExecuteScalar();
    }
}
