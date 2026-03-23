using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.Data;

namespace hotel_management;

public partial class EquipmentInRoomForm : Form
{
    public EquipmentInRoomForm()
    {
        InitializeComponent();
    }

    private void equipment_in_room_Load(object sender, EventArgs e)
    {
        LoadDevices();
    }

    private void LoadDevices(string keyword = "")
    {
        const string baseSql = """
                               SELECT CategoryId, DeviceId, DeviceName, Brand, Model, UnitPrice, PicturePath, DeviceStatus
                               FROM [Devices]
                               """;

        if (string.IsNullOrWhiteSpace(keyword))
        {
            dgvdevice.DataSource = HotelDb.Query(baseSql);
            return;
        }

        const string searchSql = """
                                 SELECT CategoryId, DeviceId, DeviceName, Brand, Model, UnitPrice, PicturePath, DeviceStatus
                                 FROM [Devices]
                                 WHERE DeviceId LIKE @search OR DeviceName LIKE @search OR Brand LIKE @search
                                 """;

        dgvdevice.DataSource = HotelDb.Query(searchSql, new SqlParameter("@search", $"%{keyword.Trim()}%"));
    }

    private void txtsearchbar_TextChanged(object sender, EventArgs e)
    {
        LoadDevices(txtsearchbar.Text);
    }

    private void btnadd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           INSERT INTO [Devices] (CategoryId, DeviceId, DeviceName, Brand, Model, UnitPrice, PicturePath, DeviceStatus)
                           VALUES (@CategoryId, @DeviceId, @DeviceName, @Brand, @Model, @UnitPrice, @PicturePath, @DeviceStatus)
                           """;

        HotelDb.Execute(sql, BuildDeviceParameters());
        LoadDevices(txtsearchbar.Text);
    }

    private void btnupd_Click(object sender, EventArgs e)
    {
        const string sql = """
                           UPDATE [Devices]
                           SET CategoryId = @CategoryId,
                               DeviceName = @DeviceName,
                               Brand = @Brand,
                               Model = @Model,
                               UnitPrice = @UnitPrice,
                               PicturePath = @PicturePath,
                               DeviceStatus = @DeviceStatus
                           WHERE DeviceId = @DeviceId
                           """;

        HotelDb.Execute(sql, BuildDeviceParameters());
        LoadDevices(txtsearchbar.Text);
    }

    private void btndel_Click(object sender, EventArgs e)
    {
        const string sql = "DELETE FROM [Devices] WHERE DeviceId = @DeviceId";
        HotelDb.Execute(sql, new SqlParameter("@DeviceId", txtdeviceID.Text.Trim()));
        LoadDevices(txtsearchbar.Text);
    }

    private SqlParameter[] BuildDeviceParameters()
    {
        decimal.TryParse(txtunitprice.Text.Trim(), out decimal unitPrice);

        return
        [
            new SqlParameter("@CategoryId", txtcategoryID.Text.Trim()),
            new SqlParameter("@DeviceId", txtdeviceID.Text.Trim()),
            new SqlParameter("@DeviceName", txtdevicename.Text.Trim()),
            new SqlParameter("@Brand", txtbrand.Text.Trim()),
            new SqlParameter("@Model", txtModel.Text.Trim()),
            new SqlParameter("@UnitPrice", unitPrice),
            new SqlParameter("@PicturePath", picdevice.ImageLocation ?? string.Empty),
            new SqlParameter("@DeviceStatus", txtdevicestatus.Text.Trim())
        ];
    }

    private void dgvdevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= dgvdevice.Rows.Count)
        {
            return;
        }

        DataGridViewRow row = dgvdevice.Rows[e.RowIndex];
        txtcategoryID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
        txtdeviceID.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
        txtdevicename.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
        txtbrand.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
        txtModel.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
        txtunitprice.Text = row.Cells[5].Value?.ToString() ?? string.Empty;
        picdevice.ImageLocation = row.Cells[6].Value?.ToString();
        txtdevicestatus.Text = row.Cells[7].Value?.ToString() ?? string.Empty;
    }
}
