using FahadDBFramework;
using FahadDBFramework.Windows;
using StudentManager.Models.Branches;
using StudentManager.Models.Users;
using StudentManager.Screens.Templates;
using StudentManager.Utilities;
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
    public partial class BranchInfoForm : TemplateForm
    {
        public BranchInfoForm()
        {
            InitializeComponent();
        }

        private void LogoPictureBox_Click(object sender, EventArgs e)
        {
            getphoto();
        }

        private void getphoto()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Logo";
            ofd.Filter = "Logo File (*.png;*.jpg;*.bmp;*.gif)|*.png;*.jpg;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LogoPictureBox.Image = new Bitmap(ofd.FileName);
            }
        }

        public int BranchId { get; set; }

        private void BranchNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TopPanelLabel.Text = BranchNameTextBox.Text;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BranchInfoForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxes();
            LoadDataAndBindToControlIfUpdate();
        }

        private void LoadDataIntoComboBoxes()
        {
            ListData.LoadDataIntoComboBox(CityComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.City });
            ListData.LoadDataIntoComboBox(DistrictComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.District });
        }

        private void LoadDataAndBindToControlIfUpdate()
        {
            if (this.IsUpdate)
            {
                DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
                DataTable dtBranch = db.GetDataList("spBranchesGetBranchDetailsByBranchId", new DbParameter { Parameter = "@BranchId", Value = this.BranchId });
                DataRow row = dtBranch.Rows[0];

                BranchNameTextBox.Text = row["BranchName"].ToString();
                EmailTextBox.Text = row["Email"].ToString();
                TelephoneTextBox.Text = row["Telephone"].ToString();
                WebsiteTextBox.Text = row["Website"].ToString();
                LogoPictureBox.Image = (row["BranchImage"] is DBNull) ? null : ImageManipulation.putPhoto((byte[])row["BranchImage"]);
                AddressTextBox.Text = row["AddressLine"].ToString();
                CityComboBox.SelectedValue = row["CityId"];
                DistrictComboBox.SelectedValue = row["DistrictId"];
                PostalTextBox.Text = row["PostalCode"].ToString();
            }
        }

        private void saveRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsformValidated())
            {
                if (this.IsUpdate)
                {
                    SaveOrUpdateRecord("spBranchesUpdateBranchDetails");
                    FDMessageBox.ShowSuccessMessage("Record is Update Successfully!");
                }
                else
                {
                    SaveOrUpdateRecord("spBranchesAddNewBranch");
                    FDMessageBox.ShowSuccessMessage("Record is Added Successfully!");
                }
                this.Close();
            }
        }

        private void SaveOrUpdateRecord(string storedprocName)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            db.SaveOrUpdateRecord(storedprocName, GetObject());
        }

        private Branch GetObject()
        {
            Branch branch = new Branch();
            branch.BranchId = (this.IsUpdate) ? this.BranchId : 0;
            branch.BranchName = BranchNameTextBox.Text;
            branch.Email = EmailTextBox.Text;
            branch.Telephone = TelephoneTextBox.Text;
            branch.Website = WebsiteTextBox.Text;
            branch.AddressLine = AddressTextBox.Text;
            branch.PostalCode = PostalTextBox.Text;
            branch.CityId = Convert.ToInt32(CityComboBox.SelectedValue);
            branch.DistrictId = Convert.ToInt32(DistrictComboBox.SelectedValue);
            branch.BranchImage = (LogoPictureBox.Image == null) ? null : ImageManipulation.getPhoto(LogoPictureBox);
            branch.CreatedBy = LoggedInUser.Username;
            return branch;
        }
        private bool IsformValidated()
        {
            if (BranchNameTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Branch Name is Required!");
                BranchNameTextBox.Focus();
                return false;
            }
            if (EmailTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Email is Required!");
                EmailTextBox.Focus();
                return false;
            }
            if (TelephoneTextBox.Text.Trim() == string.Empty)
            { 
                FDMessageBox.ShowErrorMessage("Telephone is Required!");
                TelephoneTextBox.Focus();
                return false;
            }
            return true;
        }

        private void GetPhotoPictureBox_Click(object sender, EventArgs e)
        {
            getphoto();
        }

        private void ClearPictureBox_Click(object sender, EventArgs e)
        {
            LogoPictureBox.Image = null;
        }
    }
}
