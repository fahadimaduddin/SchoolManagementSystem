using FahadDBFramework;
using FahadDBFramework.Windows;
using StudentManager.Models.Employees;
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

namespace StudentManager.Screens.Employees
{
    public partial class EmployeeInfoForm : TemplateForm
    {
        public EmployeeInfoForm()
        {
            InitializeComponent();
        }

        public int EmployeeId { get; set; }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmployeeInfoForm_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBoxes();

            if (this.IsUpdate)
            {
                LoadDataAndBindIntoControls();
                EnableButttons();
            }
            else
            {
                GenerateEmployeeId();
                SetLeavingCommentsSection();
            }
        }

        private void SetLeavingCommentsSection()
        {
            HasLeftComboBox.Text = "No";
            EnableOrDisableControls(false);
        }

        private void LoadDataAndBindIntoControls()
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            DataTable dtEmployee = db.GetDataList("spEmployeesGetEmployeeDetailsByEmployeeId", new DbParameter { Parameter = "@EmployeeId", Value = this.EmployeeId });
            DataRow row = dtEmployee.Rows[0];

            EmployeeIDTextBox.Text = row["EmployeeId"].ToString();
            FullNameTextBox.Text = row["FullName"].ToString();
            DateOfBirthDateTimePicker.Value = Convert.ToDateTime(row["DateOfBirth"]);
            CNICtextBox.Text = row["NICNumber"].ToString();
            EmailTextBox.Text = row["EmailAddress"].ToString();
            MobileTextBox.Text = row["Mobile"].ToString();
            TelephoneTextBox.Text = row["Telephone"].ToString();
            GenderComboBox.SelectedValue = row["GenderId"];
            EmploymentDateTimePicker.Value = Convert.ToDateTime(row["EmploymentDate"]);
            BranchComboBox.SelectedValue = row["BranchId"];
            PhotoPictureBox.Image = (row["Photo"] is DBNull) ? null : ImageManipulation.putPhoto((byte[])row["Photo"]);
            AddressLineTextBox.Text = row["AddressLine"].ToString();
            CityComboBox.SelectedValue = row["CityId"];
            DistrictComboBox.SelectedValue = row["DistrictId"];
            PostalCodeTextBox.Text = row["PostalCode"].ToString();
            JobTitleComboBox.SelectedValue = row["JobTitleId"];
            CurrentSalaryTextBox.Text = row["CurrentSalary"].ToString();
            StartingSalaryTextBox.Text = row["StartingSalary"].ToString();
            HasLeftComboBox.Text = (Convert.ToBoolean(row["HasLeft"]) == true) ? "Yes" : "No";
            if (row["DateLeft"] is DBNull) { DateLeftDateTimePicker.CustomFormat = " "; }
            else{  DateLeftDateTimePicker.Value = Convert.ToDateTime(row["DateLeft"]); }
            ReasonComboBox.SelectedValue = row["ReasonLeftId"];
            CommentsTextBox.Text = row["Comments"].ToString();
        }

        private void LoadDataIntoComboBoxes()
        {
            ListData.LoadDataIntoComboBox(BranchComboBox, "spBranchesGetAllBranchNames");
            ListData.LoadDataIntoComboBox(CityComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.City });
            ListData.LoadDataIntoComboBox(DistrictComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.District });
            ListData.LoadDataIntoComboBox(GenderComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.Gender });
            ListData.LoadDataIntoComboBox(JobTitleComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.EmployeeJobTitle });
            ListData.LoadDataIntoComboBox(HasLeftComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.YesNo });
            ListData.LoadDataIntoComboBox(ReasonComboBox, new DbParameter { Parameter = "@ListTypeId", Value = ListTypes.EmployeeReasonLeft });

        }

        private void GenerateEmployeeId()
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            EmployeeIDTextBox.Text = db.getScalarValue("spEmployeesGenerateNewEmployeeId").ToString();
        }
        private void getPhoto()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Photo";
            ofd.Filter = "Photo File (*.png;*.jpg;*.bmp;*.gif)|*.png;*.jpg;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PhotoPictureBox.Image = new Bitmap(ofd.FileName);
            }
        }

        private void PhotoPictureBox_Click(object sender, EventArgs e)
        {
            getPhoto();
        }

        private void GetPhotoPictureBox_Click(object sender, EventArgs e)
        {
            getPhoto();
        }

        private void ClearPictureBox_Click(object sender, EventArgs e)
        {
            PhotoPictureBox.Image = null;
        }
        private void saveRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsformValidated())
            {
                if (this.IsUpdate)
                {
                    SaveOrUpdateRecord("spEmployeesUpdateEmployeeDetails");
                    FDMessageBox.ShowSuccessMessage("Record is Update Successfully!");
                }
                else
                {
                    SaveOrUpdateRecord("spEmployeesAddNewEmployee");
                    FDMessageBox.ShowSuccessMessage("Record is Added Successfully!");
                    this.IsUpdate = true;
                    EnableButttons();
                }
            }
        }

        private void EnableButttons()
        {
            AddToolStripMenuItem.Enabled = true;
            SendToolStripMenuItem.Enabled = true;
            PrintToolStripMenuItem.Enabled = true;
        }

        private void SaveOrUpdateRecord(string storedprocName)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            db.SaveOrUpdateRecord(storedprocName, GetObject());
        }

        private Employee GetObject()
        {
            Employee emp = new Employee();
            emp.EmployeeId = Convert.ToInt32(EmployeeIDTextBox.Text);
            emp.FullName = FullNameTextBox.Text;
            emp.DateOfBirth = DateOfBirthDateTimePicker.Value.Date;
            emp.NICNumber = CNICtextBox.Text;
            emp.EmailAddress = EmailTextBox.Text;
            emp.Mobile = MobileTextBox.Text;
            emp.Telephone = TelephoneTextBox.Text;
            emp.GenderId =  (GenderComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(GenderComboBox.SelectedValue);
            emp.EmploymentDate = EmploymentDateTimePicker.Value.Date;
            emp.BranchId = (BranchComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(BranchComboBox.SelectedValue);
            emp.Photo = (PhotoPictureBox.Image == null) ? null : ImageManipulation.getPhoto(PhotoPictureBox);
            emp.AddressLine = AddressLineTextBox.Text;
            emp.CityId = (CityComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(CityComboBox.SelectedValue);
            emp.DistrictId = (DistrictComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(DistrictComboBox.SelectedValue);
            emp.PostalCode = PostalCodeTextBox.Text;
            emp.JobTitleId = (JobTitleComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(JobTitleComboBox.SelectedValue);
            emp.CurrentSalary = Convert.ToDecimal(CurrentSalaryTextBox.Text);
            emp.StartingSalary = Convert.ToDecimal(StartingSalaryTextBox.Text);
            emp.Hasleft = (HasLeftComboBox.Text == "Yes") ? true : false;
            emp.DateLeft = (HasLeftComboBox.Text == "Yes") ? DateLeftDateTimePicker.Value.Date : (DateTime?)null;
            emp.ReasonLeftId = (ReasonComboBox.SelectedIndex == -1) ? 0 : Convert.ToInt32(ReasonComboBox.SelectedValue);
            emp.Comments = CommentsTextBox.Text;
            emp.CreatedBy = LoggedInUser.Username;
            return emp;
        }

        private bool IsformValidated()
        {
            if (FullNameTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Full Name is Required!");
                FullNameTextBox.Focus();
                return false;
            }
            if (DateOfBirthDateTimePicker.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Date of Birth is Required!");
                DateOfBirthDateTimePicker.Focus();
                return false;
            }
            if ((MobileTextBox.Text.Trim() == string.Empty) && (TelephoneTextBox.Text.Trim() == string.Empty))
            {
                FDMessageBox.ShowErrorMessage("Mobile or Telephone Number is Required!");
                MobileTextBox.Focus();
                return false;
            }
            if (CNICtextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("CNIC is Required!");
                CNICtextBox.Focus();
                return false;
            }
            if (GenderComboBox.SelectedIndex == -1)
            {
                FDMessageBox.ShowErrorMessage("Gender is Required!");
                GenderComboBox.Focus();
                return false;
            }
            if (EmploymentDateTimePicker.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Employment date is Required!");
                EmploymentDateTimePicker.Focus();
                return false;
            }
            if (BranchComboBox.SelectedIndex == -1)
            {
                FDMessageBox.ShowErrorMessage("Branch Name is Required!");
                BranchComboBox.Focus();
                return false;
            }
            if (AddressLineTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Address is Required!");
                AddressLineTextBox.Focus();
                return false;
            }
            if (CityComboBox.SelectedIndex == -1)
            {
                FDMessageBox.ShowErrorMessage("City is Required!");
                CityComboBox.Focus();
                return false;
            }
            if (DistrictComboBox.SelectedIndex == -1)
            {
                FDMessageBox.ShowErrorMessage("District is Required!");
                DistrictComboBox.Focus();
                return false;
            }
            if (PostalCodeTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Postal Code is Required!");
                PostalCodeTextBox.Focus();
                return false;
            }
            if (JobTitleComboBox.SelectedIndex == -1)
            {
                FDMessageBox.ShowErrorMessage("Job Title is Required!");
                JobTitleComboBox.Focus();
                return false;
            }
            if (CurrentSalaryTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Current Salary is Required!");
                CurrentSalaryTextBox.Focus();
                return false;
            }
            else
            {
                if (Convert.ToDecimal(CurrentSalaryTextBox.Text.Trim()) < 1)
                {
                    FDMessageBox.ShowErrorMessage("Current Salary can't zero or less than one.");
                    CurrentSalaryTextBox.Focus();
                    return false;
                }
            }
            if (StartingSalaryTextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Starting Salary is Required!");
                StartingSalaryTextBox.Focus();
                return false;
            }
            else
            {
                if (Convert.ToDecimal(StartingSalaryTextBox.Text.Trim()) < 1)
                {
                    FDMessageBox.ShowErrorMessage("Starting Salary can't zero or less than one.");
                    StartingSalaryTextBox.Focus();
                    return false;
                }
            }
            if (HasLeftComboBox.Text == "Yes")
            {
                if (DateLeftDateTimePicker.Text.Trim() == string.Empty)
                {
                    FDMessageBox.ShowErrorMessage("Date left is Required!");
                    DateLeftDateTimePicker.Focus();
                    return false;
                }
                if (ReasonComboBox.SelectedIndex == -1)
                {
                    FDMessageBox.ShowErrorMessage("Leaving reason is Required!");
                    ReasonComboBox.Focus();
                    return false;
                }
                if (CommentsTextBox.Text.Trim() == string.Empty)
                {
                    FDMessageBox.ShowErrorMessage("Leaving comments is Required!");
                    CommentsTextBox.Focus();
                    return false;
                }
            }
            return true;
        }

        private void FullNameTextBox_TextChanged(object sender, EventArgs e)
        {
            TopPanelLabelName.Text = FullNameTextBox.Text;
        }

        private void HasLeftComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HasLeftComboBox.Text == "No")
            {
                DateLeftDateTimePicker.CustomFormat = " ";
                ReasonComboBox.SelectedIndex = -1;
                EnableOrDisableControls(false);
            }
            else
            {
                EnableOrDisableControls(true);
            }
        }

        private void EnableOrDisableControls(bool enable)
        {
            DateLeftDateTimePicker.Enabled = enable;
            ReasonComboBox.Enabled = enable;
            CommentsTextBox.Enabled = enable;
        }
    }
}
