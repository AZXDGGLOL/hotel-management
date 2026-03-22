using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace hotel_management
{
    internal class connect
    {
        static public SqlConnection connect_hotelDB()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=XD\SQLEXPRESS;Initial Catalog=HotelDB;Integrated Security=True;TrustServerCertificate=True;");
            return conn;
        }
    }
}
