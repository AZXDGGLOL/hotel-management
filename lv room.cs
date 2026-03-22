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
    public partial class lv_room : Form
    {
        public lv_room()
        {
            InitializeComponent();
        }
        SqlConnection? conn;
        private void lv_room_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Room Levels]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "SELECT * FROM [Room Levels] WHERE Levelid LIKE @search OR LevelName LIKE @search";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@search", "%" + txtsearchbar.Text + "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

        }

        private void refresh()
        {
            string sql = "SELECT * FROM [Room Levels]";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLVID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtLVname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtpriceperday.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO [Room Levels] (LevelId, LevelName, PricePerDay) VALUES (@id, @name, @price)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", txtLVID.Text);
            cmd.Parameters.AddWithValue("@name", txtLVname.Text);
            cmd.Parameters.AddWithValue("@price", txtpriceperday.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE [Room Levels] SET LevelName = @name, PricePerDay = @price WHERE LevelId = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", txtLVID.Text);
            cmd.Parameters.AddWithValue("@name", txtLVname.Text);
            cmd.Parameters.AddWithValue("@price", txtpriceperday.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM [Room Levels] WHERE LevelId = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", txtLVID.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }
    }
}
