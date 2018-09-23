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
    public partial class frmMailCenter : Form
    {
        public frmMailCenter()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void 接收邮箱设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmReception();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
