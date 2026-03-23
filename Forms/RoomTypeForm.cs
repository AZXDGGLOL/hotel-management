using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class RoomTypeForm : Form
{
    public RoomTypeForm()
    {
        InitializeComponent();
    }

    private void room_type_Load(object sender, EventArgs e)
    {
        LoadRoomCategories();
    }

    private void LoadRoomCategories(string keyword = "")
    {
        const string baseSql = """
                               SELECT CategoryId, CategoryName
                               FROM [Room Categories]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dgvroom.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT CategoryId, CategoryName
                                 FROM [Room Categories]
                                 WHERE CategoryId LIKE @search OR CategoryName LIKE @search
                                 """;

        dgvroom.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void txtsearchbar_TextChanged(object sender, EventArgs e)
    {
        LoadRoomCategories(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           INSERT INTO [Room Categories] (CategoryId, CategoryName)
                           VALUES (@CategoryId, @CategoryName)
                           """;

        HotelDb.Execute(
            sql,
            new SqlParameter("@CategoryId", txtCategoryID.Text.Trim()),
            new SqlParameter("@CategoryName", txtCategoryname.Text.Trim()));

        LoadRoomCategories(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           UPDATE [Room Categories]
                           SET CategoryName = @CategoryName
                           WHERE CategoryId = @CategoryId
                           """;

        HotelDb.Execute(
            sql,
            new SqlParameter("@CategoryId", txtCategoryID.Text.Trim()),
            new SqlParameter("@CategoryName", txtCategoryname.Text.Trim()));

        LoadRoomCategories(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Room Categories] WHERE CategoryId = @CategoryId";
        HotelDb.Execute(sql, new SqlParameter("@CategoryId", txtCategoryID.Text.Trim()));
        LoadRoomCategories(txtsearchbar.Text);
    }

    private void dgvroom_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvroom.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dgvroom.Rows[e.RowIndex];
        txtCategoryID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtCategoryname.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
    }
}
