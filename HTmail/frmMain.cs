using clsBuiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            //初始化信息
            clsAllnew BusinessHelp = new clsAllnew();
         
         //BusinessHelp. SendMail();
      //   BusinessHelp.CDOMessageSend();

            
          // BusinessHelp. SendMail2();   

      //   BusinessHelp.SendMailUseGmail();

  BusinessHelp.outllookSend();
         
           MessageBox.Show("ok");
        }
    }
}
