using SysArch.Forms.Modals.Department;
using SysArch.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysArch.Forms
{
    public partial class DepartmentForm : Form
    {
        public DepartmentForm()
        {
            InitializeComponent();
        }

        private void lblCollege_Click(object sender, EventArgs e)
        {
            NewCollegeForm college = new NewCollegeForm();
            college.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddDepartmentModal modal = new AddDepartmentModal();
            modal.Show();
            this.Close();
        }

        private void Department_Load(object sender, EventArgs e)
        {
            try
            {
                DepartmentService.ViewDepartment(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading department:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                if (row.IsNewRow || row.Cells["DepartmentID"].Value == null || row.Cells["DepartmentName"].Value == null || row.Cells["DepartmentCode"].Value == null ||
                    row.Cells["DepartmentIsActive"].Value == null || row.Cells["CollegeID"].Value == null || row.Cells["CollegeName"].Value == null ||
                    row.Cells["CollegeCode"].Value == null || row.Cells["CollegeIsActive"].Value == null)
                {
                    MessageBox.Show("Cannot update an empty row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int departmentId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["DepartmentID"].Value);
                string deptName = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["DepartmentName"].Value);
                string deptCode = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["DepartmentCode"].Value);
                int departmentIsActive = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["DepartmentIsActive"].Value);
                int collegeId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["CollegeID"].Value);
                string collegeName = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["CollegeName"].Value);
                string collegeCode = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["CollegeCode"].Value);

                UpdateDepartmentModal updateDept = new UpdateDepartmentModal();
                updateDept.PopulateFields(departmentId, deptName, deptCode, departmentIsActive,
                                    collegeId, collegeName, collegeCode);
                updateDept.Show();

                this.Close();

                Department_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a row to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            if (row.IsNewRow || row.Cells["DepartmentID"].Value == null || row.Cells["DepartmentName"].Value == null || row.Cells["DepartmentCode"].Value == null ||
                row.Cells["DepartmentIsActive"].Value == null || row.Cells["CollegeID"].Value == null || row.Cells["CollegeName"].Value == null ||
                row.Cells["CollegeCode"].Value == null || row.Cells["CollegeIsActive"].Value == null)
            {
                MessageBox.Show("Cannot update an empty row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var res = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                int deptId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["DepartmentID"].Value);
                int collegeId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["CollegeID"].Value);

                DepartmentService.DeleteDepartmentAndCollege(deptId, collegeId);

                MessageBox.Show("College deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Department_Load(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearchBar.Text.Trim();

                if (txtSearchBar.Text != "")
                {
                    DepartmentService.SearchDepartmentAndCollege(searchTerm, dataGridView1);
                }
                else
                {
                    MessageBox.Show("Input search bar", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Department_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearchBar.Text.Trim();

                if (txtSearchBar.Text != "")
                {
                    DepartmentService.SearchDepartmentAndCollege(searchTerm, dataGridView1);
                }
                else
                {
                    Department_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
