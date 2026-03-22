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
    public partial class checkin_out : Form
    {
        public checkin_out()
        {
            InitializeComponent();
        }
        SqlConnection? conn;

        private void checkin_out_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM checkin_out", conn);
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
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM checkin_out", conn);
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
            string sql = "SELECT * FROM checkin_out WHERE checkin_id LIKE @search OR checkout_id LIKE @search OR room_id LIKE @search OR guest_id LIKE @search";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@search", "%" + txtsearchbar.Text + "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            txtStayid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSS.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpcheckin.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            dtpCheckout.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            txtcomment.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtPaymentStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtReceiptID.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtroomID.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "INSERT INTO [Stay list] (Stayid,SS,checkin_date,checkout_date,comment,PaymentStatus,ReceiptID,room_id) VALUES (@Stayid,@SS,@checkin_date,@checkout_date,@comment,@PaymentStatus,@ReceiptID,@room_id)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Stayid", txtStayid.Text);
                cmd.Parameters.AddWithValue("@SS", txtSS.Text);
                cmd.Parameters.AddWithValue("@checkin_date", dtpcheckin.Value);
                cmd.Parameters.AddWithValue("@checkout_date", dtpCheckout.Value);
                cmd.Parameters.AddWithValue("@comment", txtcomment.Text);
                cmd.Parameters.AddWithValue("@PaymentStatus", txtPaymentStatus.Text);
                cmd.Parameters.AddWithValue("@ReceiptID", txtReceiptID.Text);
                cmd.Parameters.AddWithValue("@room_id", txtroomID.Text);
                cmd.ExecuteNonQuery();
                refresh();
        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "UPDATE [Stay list] SET SS = @SS, checkin_date = @checkin_date, checkout_date = @checkout_date, comment = @comment, PaymentStatus = @PaymentStatus, ReceiptID = @ReceiptID, room_id = @room_id WHERE Stayid = @Stayid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Stayid", txtStayid.Text);
            cmd.Parameters.AddWithValue("@SS", txtSS.Text);
            cmd.Parameters.AddWithValue("@checkin_date", dtpcheckin.Value);
            cmd.Parameters.AddWithValue("@checkout_date", dtpCheckout.Value);
            cmd.Parameters.AddWithValue("@comment", txtcomment.Text);
            cmd.Parameters.AddWithValue("@PaymentStatus", txtPaymentStatus.Text);
            cmd.Parameters.AddWithValue("@ReceiptID", txtReceiptID.Text);
            cmd.Parameters.AddWithValue("@room_id", txtroomID.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

       

        private void btndel_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "DELETE FROM [Stay list] WHERE Stayid = @Stayid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Stayid", txtStayid.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }
    }
}
