using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmTimeSelect : Form
    {
        public DateTime dateclose ;
        public frmTimeSelect()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "yyyy年MM月dd日HH时mm分";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = false;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            //dateclose = dateTimePicker1.SelectionEnd.ToString("yyyy/MM/dd");
            //string jj = dateTimePicker1.SelectionRange.Start.ToString("yyyy/MM/dd");
            //string end = dateTimePicker1.SelectionRange.End.ToString("yyyy/MM/dd");
            //if (jj != end)
            //    dateclose = dateclose.Substring(0, 7);
            dateclose = Convert.ToDateTime(this.dateTimePicker1.Value);
            this.Close();
        }

        private void Btcanel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
