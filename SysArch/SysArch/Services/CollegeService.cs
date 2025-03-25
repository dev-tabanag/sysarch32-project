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

        public static void AddCollege(string collegeName, string collegeCode, int isActive)
        {
            string query = @"INSERT INTO dbo.college (college_name, college_code, is_active) VALUES (@CollegeName, @CollegeCode, @IsActive)";

            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CollegeName", collegeName);
                    command.Parameters.AddWithValue("@CollegeCode", collegeCode);
                    command.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        DbHelpers.ModifyRecords(command);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding college: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static void UpdateCollege(int id, string collegeName, string collegeCode, int isActive)
        {
            string query = @"
UPDATE dbo.college 
SET college_name = @CollegeName,
    college_code = @CollegeCode,
    is_active = @IsActive
WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CollegeName", collegeName);
                    command.Parameters.AddWithValue("@CollegeCode", collegeCode);
                    command.Parameters.AddWithValue("@IsActive", isActive);
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        DbHelpers.ModifyRecords(command);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating college: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
