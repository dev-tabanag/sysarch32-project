using SysArch.DbHelper;
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

namespace SysArch.Forms.Modals.Department
{
    public partial class AddDepartmentModal : Form
    {
        DataTable data;
        public AddDepartmentModal()
        {
            InitializeComponent();
        }

        private void ViewDepartmentTable()
        {
            DepartmentForm dept = new DepartmentForm();
            dept.Show();
            this.Close();
        }

        private void AddDepartmentModal_Load(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();
            data = DbHelpers.FillCollegeName(dgv);
            cboCollegeName.DataSource = data;
            cboCollegeName.DisplayMember = "college_name";
            cboCollegeName.ValueMember = "id";
            cboCollegeCode.DataSource = data;
            cboCollegeCode.ValueMember = "college_code";

            cboCollegeCode.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string collegeName = cboCollegeName.Text.Trim();
            string collegeCode = cboCollegeCode.Text.Trim();
            string deptName = txtDeptName.Text.Trim();
            string deptCode = txtDeptCode.Text.Trim();
            int radioButtonYes = rbYes.Checked  ? 1 : 0;

            if (string.IsNullOrWhiteSpace(collegeName) || string.IsNullOrWhiteSpace(deptName) || string.IsNullOrWhiteSpace(deptCode))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DepartmentService.AddCollegeAndDepartment(collegeName,deptName,deptCode, radioButtonYes);
                MessageBox.Show("Department Added Successfully");
                cboCollegeName.Text= "";
                cboCollegeCode.Text= "";
                txtDeptName.Clear();
                txtDeptCode.Clear();
                rbYes.Checked = false;
                rbNo.Checked = false;
                ViewDepartmentTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcbBack_Click(object sender, EventArgs e)
        {
            ViewDepartmentTable();
        }
    }
}
