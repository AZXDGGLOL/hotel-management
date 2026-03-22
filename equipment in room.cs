using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotel_management
{
    public partial class equipment_in_room : Form
    {
        public equipment_in_room()
        {
            InitializeComponent();
        }
        SqlConnection? conn;

        private void equipment_in_room_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Devices", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvdevice.DataSource = dt;



        }
        private void refresh()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Devices", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvdevice.DataSource = dt;
        }


        private void txtsearchbar_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "SELECT * FROM Devices WHERE Deviceid LIKE @search OR DeviceName LIKE @search";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@search", "%" + txtsearchbar.Text + "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvdevice.DataSource = dt;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Devices (Categoryid,Deviceid,Devicename,Brand,Model,UnitPrice,DeviceStatus) VALUE (@Categoryid,@Deviceid,@Devicename,@Brand,@Model,@UnitPrice,@DeviceStatus)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Categoryid", txtcategoryID.Text);
            cmd.Parameters.AddWithValue("@Deviceid", txtdeviceID.Text);
            cmd.Parameters.AddWithValue("@Devicename", txtdevicename.Text);
            cmd.Parameters.AddWithValue("@Brand", txtbrand.Text);
            cmd.Parameters.AddWithValue("@Model", txtModel.Text);
            cmd.Parameters.AddWithValue("@UnitPrice", txtunitprice.Text);
            cmd.Parameters.AddWithValue("@DeviceStatus", txtdevicestatus.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Added successfully");
            refresh();

        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE Devices SET Categoryid = @Categoryid, Devicename = @Devicename, Brand = @Brand, Model = @Model, UnitPrice = @UnitPrice, DeviceStatus = @DeviceStatus WHERE Deviceid = @Deviceid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Categoryid", txtcategoryID.Text);
            cmd.Parameters.AddWithValue("@Deviceid", txtdeviceID.Text);
            cmd.Parameters.AddWithValue("@Devicename", txtdevicename.Text);
            cmd.Parameters.AddWithValue("@Brand", txtbrand.Text);
            cmd.Parameters.AddWithValue("@Model", txtModel.Text);
            cmd.Parameters.AddWithValue("@UnitPrice", txtunitprice.Text);
            cmd.Parameters.AddWithValue("@DeviceStatus", txtdevicestatus.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated successfully");
            refresh();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM Devices WHERE Deviceid = @Deviceid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Deviceid", txtdeviceID.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted successfully");
            refresh();
        }

        private void dgvdevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtdeviceID.Text = dgvdevice.CurrentRow.Cells[1].Value.ToString();
            txtdevicename.Text = dgvdevice.CurrentRow.Cells[2].Value.ToString();
            txtbrand.Text = dgvdevice.CurrentRow.Cells[3].Value.ToString();
            txtModel.Text = dgvdevice.CurrentRow.Cells[4].Value.ToString();
            txtunitprice.Text = dgvdevice.CurrentRow.Cells[5].Value.ToString();
            picdevice.ImageLocation = dgvdevice.CurrentRow.Cells[6].Value.ToString();
            txtdevicestatus.Text = dgvdevice.CurrentRow.Cells[7].Value.ToString();
            txtcategoryID.Text = dgvdevice.CurrentRow.Cells[0].Value.ToString();


        }
    }
}
