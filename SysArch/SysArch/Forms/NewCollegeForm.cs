using SysArch.Forms.Modals;
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
    public partial class NewCollegeForm : Form
    {
        public NewCollegeForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearchBar.Text.Trim();

                if (txtSearchBar.Text != "")
                {
                    CollegeService.SearchCollege(searchTerm, dataGridView1);
                }
                else
                {
                    MessageBox.Show("Input search bar", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NewCollegeForm_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCollegeModal college = new AddCollegeModal();
            college.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                if (row.IsNewRow ||
                    row.Cells["id"].Value == null || row.Cells["college_name"].Value == null ||
                    row.Cells["college_code"].Value == null || row.Cells["is_active"].Value == null)
                {
                    MessageBox.Show("Cannot update an empty row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["id"].Value);
                string collegeName = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["college_name"].Value);
                string collegeCode = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["college_code"].Value);
                int isActive = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["is_active"].Value);

                UpdateCollegeModal updateCollege = new UpdateCollegeModal();
                updateCollege.PopulateFields(id, collegeName, collegeCode, isActive);

                updateCollege.Show();
                this.Hide();

                NewCollegeForm_Load(sender, e);
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

            if (row.IsNewRow ||
                row.Cells["id"].Value == null || row.Cells["college_name"].Value == null ||
                row.Cells["college_code"].Value == null || row.Cells["is_active"].Value == null)
            {
                MessageBox.Show("Cannot delete an empty row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var res = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["id"].Value);

                CollegeService.DeleteCollege(id);

                MessageBox.Show("College deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NewCollegeForm_Load(sender, e);
            }
        }

        private void lblViewDepartment_Click(object sender, EventArgs e)
        {
            DepartmentForm dept = new DepartmentForm();
            dept.Show();
            this.Hide();
        }

        private void NewCollegeForm_Load(object sender, EventArgs e)
        {
            try
            {
                CollegeService.ViewCollege(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading college:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearchBar.Text.Trim();

                if (txtSearchBar.Text != "")
                {
                    CollegeService.SearchCollege(searchTerm, dataGridView1);
                }
                else
                {
                    NewCollegeForm_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
