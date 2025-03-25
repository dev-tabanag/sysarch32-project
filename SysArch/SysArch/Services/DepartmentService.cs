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
    public class DepartmentService
    {
        public static void ViewDepartment(DataGridView dgv)
        {
            string query = @"
                            SELECT
                                d.id AS DepartmentID,
                                d.department_name AS DepartmentName,
                                d.department_code AS DepartmentCode,
                                d.is_active AS DepartmentIsActive,
                                c.id AS CollegeID,
                                c.college_name AS CollegeName,
                                c.college_code AS CollegeCode,
                                c.is_active AS CollegeIsActive
                            FROM dbo.department d
                            JOIN dbo.college c ON d.college_id = c.id";

            using (SqlCommand command = new SqlCommand(query))
            {
                try
                {
                    DbHelpers.Fill(command, dgv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying department: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void AddCollegeAndDepartment(string collegeName, string departmentName, string departmentCode, int departmentIsActive)
        {
            string insertDepartmentQuery = @"INSERT INTO dbo.department(college_id, department_name, department_code, is_active) 
                                        VALUES (@CollegeId, @DepartmentName, @DepartmentCode, @DepartmentIsActive)";

            string getCollegeId = @"SELECT id FROM dbo.college WHERE college_name = @CollegeName";

            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                connection.Open();
                int collegeId;

                using (SqlCommand cmdCollege = new SqlCommand(getCollegeId, connection))
                {
                    cmdCollege.Parameters.AddWithValue("@CollegeName", collegeName);
                    object result = cmdCollege.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show($"College '{collegeName}' not found. Cannot add department.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    collegeId = Convert.ToInt32(result);
                }
                using (SqlCommand cmdDepartment = new SqlCommand(insertDepartmentQuery, connection))
                {
                    cmdDepartment.Parameters.AddWithValue("@CollegeId", collegeId);
                    cmdDepartment.Parameters.AddWithValue("@DepartmentName", departmentName);
                    cmdDepartment.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                    cmdDepartment.Parameters.AddWithValue("@DepartmentIsActive", departmentIsActive);

                    try
                    {
                        DbHelpers.ModifyRecords(cmdDepartment);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding department: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
