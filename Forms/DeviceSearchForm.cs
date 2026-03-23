using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class DeviceSearchForm : Form
{
    public DeviceSearchForm()
    {
        InitializeComponent();
    }

    private void device_search_Load(object sender, EventArgs e)
    {
        LoadDeviceMapping();
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        LoadDeviceMapping(textBox1.Text);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        textBox1.Text = string.Empty;
        LoadDeviceMapping();
    }

    private void LoadDeviceMapping(string roomKeyword = "")
    {
        const string baseSql = """
                               SELECT D.DeviceId,
                                      D.DeviceName,
                                      D.Brand,
                                      D.Model,
                                      D.DeviceStatus,
                                      DC.CategoryName,
                                      RD.RoomId
                               FROM [Devices] AS D
                               INNER JOIN [Device Category] AS DC ON DC.CategoryId = D.CategoryId
                               LEFT JOIN [Room Devices] AS RD ON RD.DeviceId = D.DeviceId
                               """;

        if (string.IsNullOrWhiteSpace(roomKeyword))
        {
            dataGridView1.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT D.DeviceId,
                                        D.DeviceName,
                                        D.Brand,
                                        D.Model,
                                        D.DeviceStatus,
                                        DC.CategoryName,
                                        RD.RoomId
                                 FROM [Devices] AS D
                                 INNER JOIN [Device Category] AS DC ON DC.CategoryId = D.CategoryId
                                 LEFT JOIN [Room Devices] AS RD ON RD.DeviceId = D.DeviceId
                                 WHERE RD.RoomId LIKE @search
                                 """;

        dataGridView1.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{roomKeyword.Trim()}%"));
    }
}
