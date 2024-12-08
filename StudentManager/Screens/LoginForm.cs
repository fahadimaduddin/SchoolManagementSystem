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
using FahadDBFramework;
using StudentManager.Utilities;
using StudentManager.Models.Users;
using FahadDBFramework.Windows;

namespace StudentManager.Screens
{
    public partial class LoginForm : TemplateForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SignInbutton_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
                bool IsLoginDetailsCorrect = Convert.ToBoolean(db.getScalarValue("spUsersCheckLoginDetails", getParameters()));
                if (IsLoginDetailsCorrect)
                {
                    GetLoggedInUserSettings();
                    this.Hide();
                    DashboardForm df = new DashboardForm();
                    df.Show();
                }
                else
                {
                    FDMessageBox.ShowErrorMessage("Username or Password is Not Correct!");
                }
            }
        }

        private void GetLoggedInUserSettings()
        {
            LoggedInUser.Username = UsernametextBox.Text.Trim();
        }

        private DbParameter[] getParameters()
        {
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter dbparam1 = new DbParameter();
            dbparam1.Parameter = "@username";
            dbparam1.Value = UsernametextBox.Text;
            parameters.Add(dbparam1);

            DbParameter dbparam2 = new DbParameter();
            dbparam2.Parameter = "@password";
            dbparam2.Value = PasswordtextBox.Text;
            parameters.Add(dbparam2);


            return parameters.ToArray();
        }

        private bool IsFormValid()
        {
            if (UsernametextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Username is Required!");
                UsernametextBox.Clear();
                UsernametextBox.Focus();
                return false;
            }
            if (PasswordtextBox.Text.Trim() == string.Empty)
            {
                FDMessageBox.ShowErrorMessage("Password is Required!");
                PasswordtextBox.Clear();
                PasswordtextBox.Focus();
                return false;
            }
            return true;
        }
    }
}
