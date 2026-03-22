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
    public partial class search : Form
    {
        public search()
        {
            InitializeComponent();
        }
        SqlConnection? conn;
        private void search_Load(object sender, EventArgs e)
        {
            conn = connect.connect_hotelDB();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Rooms", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (radioButton2.Checked)
            {
                SqlDataAdapter da = new SqlDataAdapter("select R.Roomid, R.Floor, RC.CategoryName,RL.LevelName,RL.PricePerDay,SL.CheckInDate,SL.CheckOutDate\r\nfrom Rooms as R \r\nleft join [Room Categories] as RC on RC.CateGoryId = R.CategoryId\r\njoin [Room Levels] as RL on RL.LevelId = R.LevelId\r\njoin [Stay List] as SL on R.RoomId = SL.RoomId WHERE SL.CheckinDate = NULL", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select R.Roomid, R.Floor, RC.CategoryName,RL.LevelName,RL.PricePerDay,SL.CheckInDate,SL.CheckOutDate\r\nfrom Rooms as R \r\nleft join [Room Categories] as RC on RC.CateGoryId = R.CategoryId\r\njoin [Room Levels] as RL on RL.LevelId = R.LevelId\r\njoin [Stay List] as SL on R.RoomId = SL.RoomId", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (radioButton2.Checked)
            {
                SqlDataAdapter da = new SqlDataAdapter("select R.Roomid, R.Floor, RC.CategoryName,RL.LevelName,RL.PricePerDay,SL.CheckInDate,SL.CheckOutDate\r\nfrom Rooms as R \r\nleft join [Room Categories] as RC on RC.CateGoryId = R.CategoryId\r\njoin [Room Levels] as RL on RL.LevelId = R.LevelId\r\njoin [Stay List] as SL on R.RoomId = SL.RoomId WHERE SL.CheckinDate = NULL", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else 
            {
                SqlDataAdapter da = new SqlDataAdapter("select R.Roomid, R.Floor, RC.CategoryName,RL.LevelName,RL.PricePerDay,SL.CheckInDate,SL.CheckOutDate\r\nfrom Rooms as R \r\nleft join [Room Categories] as RC on RC.CateGoryId = R.CategoryId\r\njoin [Room Levels] as RL on RL.LevelId = R.LevelId\r\njoin [Stay List] as SL on R.RoomId = SL.RoomId where SL.CheckinDate like '%" + textBox1.Text + "%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           string selectedCategory = comboBox1.SelectedItem.ToString();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (radioButton2.Checked)
            {
                SqlDataAdapter da = new SqlDataAdapter("select R.Roomid, R.Floor, RC.CategoryName,RL.LevelName,RL.PricePerDay,SL.CheckInDate,SL.CheckOutDate\r\nfrom Rooms as R \r\nleft join [Room Categories] as RC on RC.CateGoryId = R.CategoryId\r\njoin [Room Levels] as RL on RL.LevelId = R.LevelId\r\njoin [Stay List] as SL on R.RoomId = SL.RoomId WHERE SL.CheckinDate = NULL and RC.CategoryName like '%" + selectedCategory + "%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select R.Roomid, R.Floor, RC.CategoryName,RL.LevelName,RL.PricePerDay,SL.CheckInDate,SL.CheckOutDate\r\nfrom Rooms as R \r\nleft join [Room Categories] as RC on RC.CateGoryId = R.CategoryId\r\njoin [Room Levels] as RL on RL.LevelId = R.LevelId\r\njoin [Stay List] as SL on R.RoomId = SL.RoomId where RC.CategoryName like '%" + selectedCategory + "%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
    }
}
