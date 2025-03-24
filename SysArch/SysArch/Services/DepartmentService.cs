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

        public static void UpdateDepartmentAndCollege(int departmentId,
                                                    string departmentName,
                                                    string departmentCode,
                                                    int departmentIsActive,
                                                    int collegeId,
                                                    string collegeName,
                                                    string collegeCode)
        {

            string queryDepartment = @"
            UPDATE dbo.department
            SET department_name = @DepartmentName,
                department_code = @DepartmentCode,
                is_active = @IsActive
            WHERE id = @Id";

            string queryCollege = @"
            UPDATE dbo.college
            SET college_name = @CollegeName,
                college_code = @CollegeCode
            WHERE id = @Id";

            

            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                using (SqlCommand cmdDepartment = new SqlCommand(queryDepartment, connection))
                {
                    cmdDepartment.Parameters.AddWithValue("@DepartmentName", departmentName);
                    cmdDepartment.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                    cmdDepartment.Parameters.AddWithValue("@IsActive", departmentIsActive);
                    cmdDepartment.Parameters.AddWithValue("@Id", departmentId);

                    try
                    {
                        DbHelpers.ModifyRecords(cmdDepartment);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating college: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                using (SqlCommand cmdCollege = new SqlCommand(queryCollege, connection))
                {
                    cmdCollege.Parameters.AddWithValue("@CollegeName", collegeName);
                    cmdCollege.Parameters.AddWithValue("@CollegeCode", collegeCode);
                    cmdCollege.Parameters.AddWithValue("@Id", collegeId);

                    try
                    {
                        DbHelpers.ModifyRecords(cmdCollege);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating college: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static void DeleteDepartmentAndCollege (int deptId, int collegeId)
        {
            string deptQuery = @"DELETE FROM dbo.department WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(Connections.dbConnect))
            {
                using (SqlCommand command = new SqlCommand(deptQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", deptId);

                    try
                    {
                        connection.Open();
                        DbHelpers.ModifyRecords(command);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting department: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connection.Close();
                }
            }
        }

        public static void SearchDepartmentAndCollege(string searchTerm, DataGridView dgv)
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
                            INNER JOIN dbo.college c
                                ON d.college_id = c.id
                            WHERE d.department_name LIKE @SearchTerm
                               OR d.department_code LIKE @SearchTerm
                               OR c.college_name   LIKE @SearchTerm
                               OR c.college_code   LIKE @SearchTerm
                        ";

            using (SqlCommand command = new SqlCommand(query))
            {
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                try
                {
                    DbHelpers.Fill(command, dgv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching department & college: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
