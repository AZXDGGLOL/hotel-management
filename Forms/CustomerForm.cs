using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class CustomerForm : Form
{
    public CustomerForm()
    {
        InitializeComponent();
    }

    private void customer_Load(object sender, EventArgs e)
    {
        LoadCustomers();
    }

    private void LoadCustomers(string keyword = "")
    {
        const string baseSql = """
                               SELECT CustomerID, NationID, FirstName, LastName, Nationlity, Address, Email, Phone
                               FROM [Member]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dataGridView1.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT CustomerID, NationID, FirstName, LastName, Nationlity, Address, Email, Phone
                                 FROM [Member]
                                 WHERE CustomerID LIKE @search
                                    OR FirstName LIKE @search
                                    OR LastName LIKE @search
                                    OR Phone LIKE @search
                                 """;

        dataGridView1.DataSource = HotelDb.Query(
            searchSql,
            new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void txtsearchbar_TextChanged(object sender, EventArgs e)
    {
        LoadCustomers(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           INSERT INTO [Member] (CustomerID, NationID, FirstName, LastName, Nationlity, Address, Email, Phone)
                           VALUES (@CustomerID, @NationID, @FirstName, @LastName, @Nationlity, @Address, @Email, @Phone)
                           """;

        HotelDb.Execute(sql, BuildCustomerParameters());
        LoadCustomers(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           UPDATE [Member]
                           SET NationID = @NationID,
                               FirstName = @FirstName,
                               LastName = @LastName,
                               Nationlity = @Nationlity,
                               Address = @Address,
                               Email = @Email,
                               Phone = @Phone
                           WHERE CustomerID = @CustomerID
                           """;

        HotelDb.Execute(sql, BuildCustomerParameters());
        LoadCustomers(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Member] WHERE CustomerID = @CustomerID";
        HotelDb.Execute(sql, new SqlParameter("@CustomerID", txtMemberID.Text.Trim()));
        LoadCustomers(txtsearchbar.Text);
    }

    private SqlParameter[] BuildCustomerParameters()
    {
        return
        [
            new SqlParameter("@CustomerID", txtMemberID.Text.Trim()),
            new SqlParameter("@NationID", txtNationID.Text.Trim()),
            new SqlParameter("@FirstName", txtFirst.Text.Trim()),
            new SqlParameter("@LastName", txtlast.Text.Trim()),
            new SqlParameter("@Nationlity", txtNationlity.Text.Trim()),
            new SqlParameter("@Address", txtAddress.Text.Trim()),
            new SqlParameter("@Email", txtEmail.Text.Trim()),
            new SqlParameter("@Phone", txtPhone.Text.Trim())
        ];
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        txtMemberID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtNationID.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
        txtFirst.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
        txtlast.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
        txtNationlity.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
        txtAddress.Text = row.Cells[5].Value?.ToString() ?? string.Empty;
        txtEmail.Text = row.Cells[6].Value?.ToString() ?? string.Empty;
        txtPhone.Text = row.Cells[7].Value?.ToString() ?? string.Empty;
    }
}
