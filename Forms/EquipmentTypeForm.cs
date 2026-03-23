using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class EquipmentTypeForm : Form
{
    public EquipmentTypeForm()
    {
        InitializeComponent();
    }

    private void equipment_type_Load(object sender, EventArgs e)
    {
        LoadCategories();
    }

    private void LoadCategories(string keyword = "")
    {
        const string baseSql = """
                               SELECT CategoryId, CategoryName
                               FROM [Device Category]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dgvDevice.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT CategoryId, CategoryName
                                 FROM [Device Category]
                                 WHERE CategoryId LIKE @search OR CategoryName LIKE @search
                                 """;

        dgvDevice.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void txtsearchbar_TextChanged(object sender, EventArgs e)
    {
        LoadCategories(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = "INSERT INTO [Device Category] (CategoryId, CategoryName) VALUES (@CategoryId, @CategoryName)";
        HotelDb.Execute(
            sql,
            new SqlParameter("@CategoryId", txtid.Text.Trim()),
            new SqlParameter("@CategoryName", txtname.Text.Trim()));

        LoadCategories(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = "UPDATE [Device Category] SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";
        HotelDb.Execute(
            sql,
            new SqlParameter("@CategoryId", txtid.Text.Trim()),
            new SqlParameter("@CategoryName", txtname.Text.Trim()));

        LoadCategories(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Device Category] WHERE CategoryId = @CategoryId";
        HotelDb.Execute(sql, new SqlParameter("@CategoryId", txtid.Text.Trim()));
        LoadCategories(txtsearchbar.Text);
    }

    private void dgvDevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvDevice.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dgvDevice.Rows[e.RowIndex];
        txtid.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtname.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
    }
}
