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
    public partial class UpdateDepartmentModal : Form
    {
        private int _deptId;
        private string _deptName;
        private string _deptCode;
        private int _deptIsActive;
        private int _collegeId;
        private string _collegeName;
        private string _collegeCode;
        public UpdateDepartmentModal()
        {
            InitializeComponent();
        }

        private void ViewDeparmentTable ()
        {
            DepartmentForm departmentForm = new DepartmentForm();
            departmentForm.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int deptId = Convert.ToInt32(txtDepartmentId.Text);
            string deptName = txtDeptName.Text;
            string deptCode = txtDeptCode.Text;
            int deptIsActive = rbYes.Checked ? 1 : 0;
            int collegeId = Convert.ToInt32(txtCollegeId.Text);
            string collegeName = cboCollegeName.Text.Trim();
            string collegeCode = cboCollegeCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(deptName) || string.IsNullOrWhiteSpace(deptCode) || string.IsNullOrWhiteSpace(deptIsActive.ToString())
                || string.IsNullOrWhiteSpace(collegeName) || string.IsNullOrWhiteSpace(collegeCode))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DepartmentService.UpdateDepartmentAndCollege(deptId, deptName, deptCode, deptIsActive, collegeId, collegeName, collegeCode);
                MessageBox.Show("Update Successful");
                ViewDeparmentTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while updating department: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PopulateFields(int deptId, string deptName, string deptCode, int deptIsActive,
                                   int collegeId, string collegeName, string collegeCode)
        {
            _deptId = deptId;
            _deptName = deptName;
            _deptCode = deptCode;
            _deptIsActive = deptIsActive;
            _collegeId = collegeId;
            _collegeName = collegeName;
            _collegeCode = collegeCode;
        }

        private void pcbBack_Click(object sender, EventArgs e)
        {
            ViewDeparmentTable();
        }

        private void UpdateDepartmentModal_Load(object sender, EventArgs e)
        {
            DataTable data = DbHelpers.FillCollegeName(new DataGridView());
            cboCollegeName.DataSource = data;
            cboCollegeName.DisplayMember = "college_name";
            cboCollegeName.ValueMember = "id";

            cboCollegeCode.DataSource = data;
            cboCollegeCode.DisplayMember = "college_code";
            cboCollegeCode.ValueMember = "id";

            txtCollegeId.Enabled = false;
            cboCollegeCode.Enabled = false;

            txtDepartmentId.Text = _deptId.ToString();
            txtDeptName.Text = _deptName.ToString();
            txtDeptCode.Text = _deptCode.ToString();

            rbYes.Checked = (_deptIsActive == 1);
            rbNo.Checked = (_deptIsActive == 0);

            txtCollegeId.Text = _collegeId.ToString();

            cboCollegeName.SelectedValue = _collegeId;

            cboCollegeCode.Text = _collegeCode;
        }
    }
}
