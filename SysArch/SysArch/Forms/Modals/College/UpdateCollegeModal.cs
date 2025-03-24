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

namespace SysArch.Forms.Modals
{
    public partial class UpdateCollegeModal : Form
    {
        public UpdateCollegeModal()
        {
            InitializeComponent();
        }

        private void ViewCollegeTable ()
        {
            CollegeForm form = new CollegeForm();
            form.Show();
            this.Close();
        }

        public void PopulateFields (int id, string collegeName, string collegeCode, int isActive)
        {
            txtId.Text = Convert.ToInt32(id).ToString();
            txtCollegeName.Text = collegeName.ToString();
            txtCollegeCode.Text = collegeCode.ToString();
            cmbIsActive.Text = isActive.ToString();
        }

        private void pcbBack_Click(object sender, EventArgs e)
        {
            ViewCollegeTable();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            string collegeName = txtCollegeName.Text;
            string collegeCode = txtCollegeCode.Text;
            int isActive = cmbIsActive.Text == "1" ? 1 : 0;

            if (string.IsNullOrWhiteSpace(collegeName) || string.IsNullOrWhiteSpace(collegeCode) || string.IsNullOrWhiteSpace(isActive.ToString()))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CollegeService.UpdateCollege(id, collegeName, collegeCode, isActive);
                MessageBox.Show("College updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ViewCollegeTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while updating college: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCollegeModal_Load(object sender, EventArgs e)
        {

        }
    }
}
