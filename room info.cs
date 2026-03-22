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
    public partial class room_info : Form
    {
        public room_info()
        {
            InitializeComponent();
        }
        SqlConnection? conn;

        private void room_info_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Room]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void refresh()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [Room]", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void txtsearchbar_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "SELECT * FROM Rooms WHERE Roomid LIKE @search OR RoomType LIKE @search";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@search", "%" + txtsearchbar.Text + "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "INSERT INTO Rooms (Roomid, Floor, LevelId, Categoryid) VALUES (@Roomid, @RoomType, @RoomPrice)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Roomid", txtRoomID.Text);
            cmd.Parameters.AddWithValue("@Floor", txtFloor.Text);
            cmd.Parameters.AddWithValue("@LevelId", txtLevelID.Text);
            cmd.Parameters.AddWithValue("@Categoryid", txtCategoryid.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "UPDATE Rooms SET Floor = @Floor, LevelId = @LevelId, Categoryid = @Categoryid WHERE Roomid = @Roomid";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Roomid", txtRoomID.Text);
                cmd.Parameters.AddWithValue("@Floor", txtFloor.Text);
                cmd.Parameters.AddWithValue("@LevelId", txtLevelID.Text);
                cmd.Parameters.AddWithValue("@Categoryid", txtCategoryid.Text);
                cmd.ExecuteNonQuery();
                refresh();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "DELETE FROM Rooms WHERE Roomid = @Roomid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Roomid", txtRoomID.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryid.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFloor.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtLevelID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtRoomID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void txtLevelID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
