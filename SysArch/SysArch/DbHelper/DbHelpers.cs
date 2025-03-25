using SysArch.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysArch.DbHelper
{
    public class DbHelpers
    {
        public static SqlConnection conn;
        public static SqlCommand command;
        public static SqlDataReader reader;

        public static DataTable FillCollegeName(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                string query = "SELECT * FROM dbo.college";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        DataTable dataTable = new DataTable();
                        try
                        {
                            adapter.Fill(dataTable);
                            dataGridView.DataSource = dataTable;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error populating DataGridView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return dataTable;

                    }
                }
            }
        }

    }
}
