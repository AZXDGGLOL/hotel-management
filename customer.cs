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
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
        }
        SqlConnection? conn;

        private void customer_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM customer", conn);
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
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM customer", conn);
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
            string sql = "SELECT * FROM customer WHERE CustomerID LIKE @search OR CustomerName LIKE @search";
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
            txtMemberID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNationID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtFirst.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtlast.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtNationlity.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
            }
            string sql = "INSERT INTO Member (CustomerID,NationID,FirstName,LastName,Nationlity,Address,Email,Phone) VALUES (@CustomerID,@NationID,@FirstName,@LastName,@Nationlity,@Address,@Email,@Phone)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerID", txtMemberID.Text);
                cmd.Parameters.AddWithValue("@NationID", txtNationID.Text);
                cmd.Parameters.AddWithValue("@FirstName", txtFirst.Text);
                cmd.Parameters.AddWithValue("@LastName", txtlast.Text);
                cmd.Parameters.AddWithValue("@Nationlity", txtNationlity.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.ExecuteNonQuery();
            refresh();
        }

        private void btnupd_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "UPDATE Member SET NationID = @NationID,FirstName = @FirstName,LastName = @LastName,Nationlity = @Nationlity,Address = @Address,Email = @Email,Phone = @Phone WHERE CustomerID = @CustomerID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CustomerID", txtMemberID.Text);
            cmd.Parameters.AddWithValue("@NationID", txtNationID.Text);
            cmd.Parameters.AddWithValue("@FirstName", txtFirst.Text);
            cmd.Parameters.AddWithValue("@LastName", txtlast.Text);
            cmd.Parameters.AddWithValue("@Nationlity", txtNationlity.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = "DELETE FROM Member WHERE CustomerID = @CustomerID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CustomerID", txtMemberID.Text);
            cmd.ExecuteNonQuery();
            refresh();
        }
    }
}
