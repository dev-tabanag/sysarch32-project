using SysArch.Connection;
using SysArch.DbHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysArch.Services
{
    public class CollegeService
    {
        public static void ViewCollege(DataGridView dgv)
        {
            string query = "SELECT * FROM dbo.college";

            using (SqlCommand command = new SqlCommand(query))
            {
                try
                {
                    DbHelpers.Fill(command, dgv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying college: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
