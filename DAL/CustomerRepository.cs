using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Domain;

namespace hotel_management.DAL;

public sealed class CustomerRepository
{
    public DataTable GetAll(string keyword = "")
    {
        const string sql = """
                           SELECT MemberId,
                                  NationalId,
                                  Name,
                                  LastName,
                                  Nationality,
                                  Address,
                                  Phone,
                                  Email
                           FROM [Member]
                           WHERE @Keyword = N''
                              OR Name LIKE @Search
                              OR LastName LIKE @Search
                              OR Phone LIKE @Search
                           ORDER BY MemberId
                           """;
        string cleanKeyword = keyword.Trim();
        return SqlDataAccess.Query(sql,
            new SqlParameter("@Keyword", cleanKeyword),
            new SqlParameter("@Search", $"%{cleanKeyword}%"));
    }

    public DataTable GetLookup()
    {
        const string sql = """
                           SELECT MemberId,
                                  Name + N' ' + LastName + N' (' + CAST(MemberId AS NVARCHAR(20)) + N')' AS MemberLabel
                           FROM [Member]
                           ORDER BY Name, LastName
                           """;
        return SqlDataAccess.Query(sql);
    }

    public int Add(Customer customer)
    {
        const string sql = """
                           INSERT INTO [Member] (NationalId, Name, LastName, Nationality, Address, Phone, Email)
                           VALUES (@NationalId, @Name, @LastName, @Nationality, @Address, @Phone, @Email)
                           """;
        return SqlDataAccess.Execute(sql, BuildInsertParameters(customer));
    }

    public int Update(Customer customer)
    {
        const string sql = """
                           UPDATE [Member]
                           SET NationalId = @NationalId,
                               Name = @Name,
                               LastName = @LastName,
                               Nationality = @Nationality,
                               Address = @Address,
                               Phone = @Phone,
                               Email = @Email
                           WHERE MemberId = @MemberId
                           """;
        return SqlDataAccess.Execute(sql, BuildUpdateParameters(customer));
    }

    public int Delete(string memberId)
    {
        const string sql = "DELETE FROM [Member] WHERE MemberId = @MemberId";
        return SqlDataAccess.Execute(sql, new SqlParameter("@MemberId", memberId));
    }

    private static SqlParameter[] BuildInsertParameters(Customer customer)
    {
        return
        [
            new SqlParameter("@NationalId", customer.NationalId),
            new SqlParameter("@Name", customer.Name),
            new SqlParameter("@LastName", customer.LastName),
            new SqlParameter("@Nationality", (object?)customer.Nationality ?? DBNull.Value),
            new SqlParameter("@Address", (object?)customer.Address ?? DBNull.Value),
            new SqlParameter("@Phone", (object?)customer.Phone ?? DBNull.Value),
            new SqlParameter("@Email", (object?)customer.Email ?? DBNull.Value)
        ];
    }

    private static SqlParameter[] BuildUpdateParameters(Customer customer)
    {
        List<SqlParameter> list = new(BuildInsertParameters(customer))
        {
            new SqlParameter("@MemberId", customer.MemberId)
        };
        return list.ToArray();
    }
}
