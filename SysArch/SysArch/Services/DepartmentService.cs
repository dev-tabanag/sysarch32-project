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

    }
}
