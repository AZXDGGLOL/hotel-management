using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class RoomLevelForm : Form
{
    public RoomLevelForm()
    {
        InitializeComponent();
    }

    private void lv_room_Load(object sender, EventArgs e)
    {
        LoadLevels();
    }

    private void LoadLevels(string keyword = "")
    {
        const string baseSql = """
                               SELECT LevelId, LevelName, PricePerDay
                               FROM [Room Levels]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dataGridView1.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT LevelId, LevelName, PricePerDay
                                 FROM [Room Levels]
                                 WHERE LevelId LIKE @search OR LevelName LIKE @search
                                 """;

        dataGridView1.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        LoadLevels(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           INSERT INTO [Room Levels] (LevelId, LevelName, PricePerDay)
                           VALUES (@LevelId, @LevelName, @PricePerDay)
                           """;

        HotelDb.Execute(sql, BuildLevelParameters());
        LoadLevels(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           UPDATE [Room Levels]
                           SET LevelName = @LevelName, PricePerDay = @PricePerDay
                           WHERE LevelId = @LevelId
                           """;

        HotelDb.Execute(sql, BuildLevelParameters());
        LoadLevels(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Room Levels] WHERE LevelId = @LevelId";
        HotelDb.Execute(sql, new SqlParameter("@LevelId", txtLVID.Text.Trim()));
        LoadLevels(txtsearchbar.Text);
    }

    private SqlParameter[] BuildLevelParameters()
    {
        decimal.TryParse(txtpriceperday.Text.Trim(), out decimal pricePerDay);
        return
        [
            new SqlParameter("@LevelId", txtLVID.Text.Trim()),
            new SqlParameter("@LevelName", txtLVname.Text.Trim()),
            new SqlParameter("@PricePerDay", pricePerDay)
        ];
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        txtLVID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtLVname.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
        txtpriceperday.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
    }
}
