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
    public partial class device_search : Form
    {
        public device_search()
        {
            InitializeComponent();
        }
        SqlConnection? conn;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "select D.DeviceName, D.Brand, D.Model, D.DeviceStatus, DC.CategoryName,RD.RoomId\r\nfrom Devices as D \r\njoin [Device Category] as DC on D.CategoryId = DC.CategoryId\r\njoin [Room Devices] as RD on RD.DeviceId = D.DeviceId\r\nwhere RD.RoomId like @search";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@search", "%" + textBox1.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void device_search_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("select D.DeviceName, D.Brand, D.Model, D.DeviceStatus, DC.CategoryName,RD.RoomId\r\nfrom Devices as D \r\njoin [Device Category] as DC on D.CategoryId = DC.CategoryId\r\njoin [Room Devices] as RD on RD.DeviceId = D.DeviceId", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select D.DeviceName, D.Brand, D.Model, D.DeviceStatus, DC.CategoryName,RD.RoomId\r\nfrom Devices as D \r\njoin [Device Category] as DC on D.CategoryId = DC.CategoryId\r\njoin [Room Devices] as RD on RD.DeviceId = D.DeviceId";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
