using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysArch.Connection
{
    public class Connections
    {
        public static SqlConnection conn;
        public static string dbConnect = "Server=LAPTOP-37GEEPRC;Database=CollegeMatrix;User Id=sa;Password=root";

        public static void DB()
        {
            try
            {
                conn = new SqlConnection(dbConnect);
                conn.Open();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
