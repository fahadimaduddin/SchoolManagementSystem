using FahadDBFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager.Utilities.Lists
{
    public class ListData
    {
        public static void LoadDataIntoDataGridView(DataGridView dgv, string storedProceName)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            dgv.DataSource = db.GetDataList(storedProceName);

            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public static void LoadDataIntoComboBox(ComboBox cb, DbParameter parameter)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList("spListTypesDataGetDataByListTypeId", parameter);
            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";
            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public static void LoadDataIntoComboBox(ComboBox cb, string storedProcName)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList(storedProcName);
            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";
            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public static void LoadDataIntoComboBox(ComboBox cb, string storedProcName,DbParameter parameter)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList(storedProcName, parameter);
            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";
            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public static void LoadDataIntoComboBox(ComboBox cb, string storedProcName, DbParameter[] parameters)
        {
            DBSqlServer db = new DBSqlServer(AppSetting.ConnectionString());
            cb.DataSource = db.GetDataList(storedProcName, parameters);
            cb.DisplayMember = "Description";
            cb.ValueMember = "Id";
            cb.SelectedIndex = -1;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
