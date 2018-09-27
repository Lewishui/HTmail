using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Order.Common;
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

        private void 发送邮箱设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmFromReception();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void 发信模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmMailTemplate();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            var form = new frmSendpage();

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void 定时发信ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
