using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class RoomInfoForm : Form
{
    public RoomInfoForm()
    {
        InitializeComponent();
    }

    private void room_info_Load(object sender, EventArgs e)
    {
        LoadRooms();
    }

    private void LoadRooms(string keyword = "")
    {
        const string baseSql = """
                               SELECT RoomId, Floor, LevelId, CategoryId
                               FROM [Rooms]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dataGridView1.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT RoomId, Floor, LevelId, CategoryId
                                 FROM [Rooms]
                                 WHERE RoomId LIKE @search
                                    OR CONVERT(varchar(50), Floor) LIKE @search
                                    OR LevelId LIKE @search
                                    OR CategoryId LIKE @search
                                 """;

        dataGridView1.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void txtsearchbar_TextChanged(object sender, EventArgs e)
    {
        LoadRooms(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           INSERT INTO [Rooms] (RoomId, Floor, LevelId, CategoryId)
                           VALUES (@RoomId, @Floor, @LevelId, @CategoryId)
                           """;

        HotelDb.Execute(sql, BuildRoomParameters());
        LoadRooms(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           UPDATE [Rooms]
                           SET Floor = @Floor,
                               LevelId = @LevelId,
                               CategoryId = @CategoryId
                           WHERE RoomId = @RoomId
                           """;

        HotelDb.Execute(sql, BuildRoomParameters());
        LoadRooms(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Rooms] WHERE RoomId = @RoomId";
        HotelDb.Execute(sql, new SqlParameter("@RoomId", txtRoomID.Text.Trim()));
        LoadRooms(txtsearchbar.Text);
    }

    private SqlParameter[] BuildRoomParameters()
    {
        int.TryParse(txtFloor.Text.Trim(), out int floor);

        return
        [
            new SqlParameter("@RoomId", txtRoomID.Text.Trim()),
            new SqlParameter("@Floor", floor),
            new SqlParameter("@LevelId", txtLevelID.Text.Trim()),
            new SqlParameter("@CategoryId", txtCategoryid.Text.Trim())
        ];
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        txtRoomID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtFloor.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
        txtLevelID.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
        txtCategoryid.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
    }

    private void txtLevelID_TextChanged(object sender, EventArgs e)
    {
    }
}
