using StudentManager.Screens.Templates;
using StudentManager.Utilities.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager.Screens.Branches
{
    public partial class ManageBranchesForm : TemplateForm
    {
        public ManageBranchesForm()
        {
            InitializeComponent();
        }

        private void AddNewBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowBranchInfoScreen(0, false);
        }

        private void ShowBranchInfoScreen(int branchId, bool isUpdate)
        {
            BranchInfoForm bif = new BranchInfoForm();
            bif.BranchId = branchId;
            bif.IsUpdate = isUpdate;
            bif.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageBranchesForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            ListData.LoadDataIntoDataGridView(BranchesDataGridView, "spBranchesGetAllBranches");
        }

        private void BranchesDataGridView_DoubleClick(object sender, EventArgs e)
        {
            int rowIndex = BranchesDataGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int branchId = Convert.ToInt32(BranchesDataGridView.Rows[rowIndex].Cells["BranchId"].Value);
            ShowBranchInfoScreen(branchId,true);
        }
    }
}
