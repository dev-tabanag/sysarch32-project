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

        public static void Fill(SqlCommand command, DataGridView dgv)
        {
            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                command.Connection = connection;

                try
                {
                    connection.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dt);

                    dgv.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error populating DataGridView: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void ModifyRecords(SqlCommand command)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Connections.dbConnect))
                {
                    conn.Open();
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while executing query:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
