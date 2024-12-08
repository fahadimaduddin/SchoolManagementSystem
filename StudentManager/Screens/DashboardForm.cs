using StudentManager.Screens.Branches;
using StudentManager.Screens.Employees;
using StudentManager.Screens.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager.Screens
{
    public partial class DashboardForm : TemplateForm
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageUsersToolStripButton_Click(object sender, EventArgs e)
        {
            ManageEmployeesForm mef = new ManageEmployeesForm();
            mef.ShowDialog();
        }

        private void ManageBranchesToolStripButton_Click(object sender, EventArgs e)
        {
            ManageBranchesForm mbf = new ManageBranchesForm();
            mbf.ShowDialog();
        }
    }
}
