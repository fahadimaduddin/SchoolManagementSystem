using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager.WinControls
{
    public partial class FDDateTimePicker : DateTimePicker
    {
        public FDDateTimePicker()
        {
            InitializeComponent();
        }

        public enum DataFormat
        {
            British, // dd/MM/yyyy (31/12/2021)
            British2, // dd-MM-yyyy (31-12-2021)
            British3, // dd/MM/yy (31/12/21)
            US, // MM/dd/yyyy (12/31/2021)
            ANSI // yyyy.MM.dd (2021.12.31)
        }
        private DataFormat dataFormat = DataFormat.British;
        [Category("Custom Date")]
        [Description("Show date in one of the following format." + "\nBritish: dd/MM/yyyy (31/12/2021)" + "\nBritish2: dd-MM-yyyy (31-12-2021)" + "\nBritish3: dd/MM/yy (31/12/21)" + "\nUS: MM/dd/yyyy (12/31/2021)" + "\nANSI: yyyy.MM.dd (2021.12.31)")]
        public DataFormat DateFormat
        {
            get { return dataFormat; }
            set
            {
                dataFormat = value;
                if (!MakeEmpty)
                {
                    SetDateFormat();
                }
            }
        }

        private void SetDateFormat()
        {
            if (DateFormat == DataFormat.British)
            {
                this.CustomFormat = "dd/MM/yyyy";
            }
            if (DateFormat == DataFormat.British2)
            {
                this.CustomFormat = "dd-MM-yyyy";
            }
            if (DateFormat == DataFormat.British3)
            {
                this.CustomFormat = "dd/MM/yy";
            }
            if (DateFormat == DataFormat.US)
            {
                this.CustomFormat = "MM/dd/yyyy";
            }
            if (DateFormat == DataFormat.ANSI)
            {
                this.CustomFormat = "yyyy.MM.dd";
            }
        }

        private bool makeEmpty = false;
        [Category("Custom Date")]
        [Description("Make control empty.")]
        public bool MakeEmpty 
        { get { return makeEmpty; }
            set
            {
                makeEmpty = value;
                if (makeEmpty == true)
                {
                    CustomFormat = " ";
                    Format = DateTimePickerFormat.Custom;
                }
                if (makeEmpty == false)
                {
                    SetDateFormat();
                    Format = DateTimePickerFormat.Custom;
                }
            } 
        }

        private void FDDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (MakeEmpty)
            {
                SetDateFormat();
            }
        }

        private void FDDateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (MakeEmpty)
            {
                if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
                {
                    this.CustomFormat = " ";
                }
            } 
        }
        [Browsable(false)]
        public new DateTimePickerFormat Format
        {
            get { return base.Format; }
            set { base.Format = value; }
        }
        [Browsable(false)]
        public new string CustomFormat
        {
            get { return base.CustomFormat; }
            set { base.CustomFormat = value; }
        }
    }
}
