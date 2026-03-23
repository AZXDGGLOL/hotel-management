using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class RoomSearchForm : Form
{
    public RoomSearchForm()
    {
        InitializeComponent();
    }

    private void search_Load(object sender, EventArgs e)
    {
        LoadCategoryFilter();
        LoadRooms();
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
        LoadRooms();
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        LoadRooms();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRooms();
    }

    private void LoadCategoryFilter()
    {
        const string sql = "SELECT CategoryName FROM [Room Categories] ORDER BY CategoryName";
        DataTable categories = HotelDb.Query(sql);

        comboBox1.Items.Clear();
        comboBox1.Items.Add("All");
        foreach (DataRow row in categories.Rows)
        {
            string? categoryName = row["CategoryName"]?.ToString();
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                comboBox1.Items.Add(categoryName);
            }
        }

        comboBox1.SelectedIndex = 0;
    }

    private void LoadRooms()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();

        string sql = """
                     SELECT R.RoomId,
                            R.Floor,
                            RC.CategoryName,
                            RL.LevelName,
                            RL.PricePerDay,
                            SL.SS AS OccupantMemberId,
                            SL.CheckInDate,
                            SL.CheckOutDate
                     FROM [Rooms] AS R
                     LEFT JOIN [Room Categories] AS RC ON RC.CategoryId = R.CategoryId
                     LEFT JOIN [Room Levels] AS RL ON RL.LevelId = R.LevelId
                     LEFT JOIN [Stay List] AS SL ON SL.RoomId = R.RoomId AND SL.CheckOutDate IS NULL
                     WHERE 1 = 1
                     """;

        if (radioButton1.Checked)
        {
            sql += " AND SL.StayId IS NULL";
        }
        else if (radioButton2.Checked)
        {
            sql += " AND SL.StayId IS NOT NULL";
        }

        if (comboBox1.SelectedItem is string selectedCategory &&
            !string.Equals(selectedCategory, "All", StringComparison.OrdinalIgnoreCase))
        {
            sql += " AND RC.CategoryName = @category";
            parameters.Add(new SqlParameter("@category", selectedCategory));
        }

        string keyword = textBox1.Text.Trim();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            sql += """
                    AND (
                        SL.SS LIKE @keyword
                        OR CONVERT(varchar(10), SL.CheckInDate, 120) LIKE @keyword
                    )
                   """;
            parameters.Add(new SqlParameter("@keyword", $"%{keyword}%"));
        }

        dataGridView1.DataSource = HotelDb.Query(sql, parameters.ToArray());
    }
}
