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
    public partial class AddCollegeModal : Form
    {
        public AddCollegeModal()
        {
            InitializeComponent();
        }

        private void ViewCollegeTable()
        {
            NewCollegeForm collegeForm = new NewCollegeForm();
            collegeForm.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string collegeName = txtCollegeName.Text;
            string collegeCode = txtCollegeCode.Text;
            int isActive = cmbIsActive.Text == "1" ? 1 : 0;

            if (string.IsNullOrWhiteSpace(collegeName) || string.IsNullOrWhiteSpace(collegeCode) || string.IsNullOrWhiteSpace(cmbIsActive.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                CollegeService.AddCollege(collegeName, collegeCode, isActive);
                MessageBox.Show("College added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCollegeName.Clear();
                txtCollegeCode.Clear();
                cmbIsActive.Text = "";
                ViewCollegeTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pcbBack_Click(object sender, EventArgs e)
        {
            ViewCollegeTable();
        }
    }
}
