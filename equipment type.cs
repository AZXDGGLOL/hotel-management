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
    public partial class equipment_type : Form
    {
        public equipment_type()
        {
            InitializeComponent();
        }
        SqlConnection? conn;

        private void equipment_type_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Device Category]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDevice.DataSource = dt;
        }
        private void refresh()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Device Category]", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvDevice.DataSource = dt;
        }
        private void txtsearchbar_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "SELECT * FROM [Device Category] WHERE Categoryid LIKE @search OR CategoryName LIKE @search";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@search", "%" + txtsearchbar.Text + "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvDevice.DataSource = dt;
        }


        private void btnadd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "INSERT INTO [Device Category] (Categoryid,CategoryName) VALUES (@Categoryid,@CategoryName)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Categoryid", txtid.Text);
            cmd.Parameters.AddWithValue("@CategoryName", txtname.Text);
            cmd.ExecuteNonQuery();

            refresh();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "DELETE FROM [Device Category] WHERE Categoryid = @Categoryid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Categoryid", txtid.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "UPDATE [Device Category] SET CategoryName = @CategoryName WHERE Categoryid = @Categoryid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Categoryid", txtid.Text);
            cmd.Parameters.AddWithValue("@CategoryName", txtname.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void dgvDevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            txtid.Text = dgvDevice.CurrentRow.Cells[0].Value.ToString();
            txtname.Text = dgvDevice.CurrentRow.Cells[1].Value.ToString();
        }

        
    }
}
