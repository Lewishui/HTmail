using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmSendpage : Form
    {

        string path;

        public frmSendpage()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "");
            System.Diagnostics.Process.Start("stop Q.exe", ZFCEPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog tbox = new OpenFileDialog();
            tbox.Multiselect = false;
            tbox.Filter = "所有文件|*.*";
            tbox.Multiselect = true;
            tbox.SupportMultiDottedExtensions = true;
            if (tbox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listBox1.Items.Clear();

                foreach (string s in tbox.SafeFileNames)
                {
                    listBox1.Items.Add(s);
                }
            }            
        }
    }
}
