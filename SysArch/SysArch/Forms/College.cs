using SysArch.Forms.Modals;
using SysArch.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysArch.Forms
{
    public partial class CollegeForm : Form
    {
        public CollegeForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCollegeModal college = new AddCollegeModal();
            college.Show();
            this.Hide();
        }

        private void College_Load(object sender, EventArgs e)
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

        private void CollegeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
