using clsBuiness;
using HT.DB;
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
    public partial class frmSendTo : Form
    {

        List<Addconnect_info> userlist_Server;
        string groupID;

        public frmSendTo(string groupID1)
        {
            InitializeComponent();
            groupID = groupID1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
 
                clear();

          
        }

        private void clear()
        {
            this.txadress.Text = "";
            this.tshuihao.Text = "";
            this.txbank.Text = "";
            txaccount.Text = "";
            txphone.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            read();
            clsAllnew BusinessHelp = new clsAllnew();

            int ISURN = BusinessHelp.create_Addconnect_Server(userlist_Server);
            if (ISURN == 1)
            {

                if (MessageBox.Show(" 创建成功 , 是否继续添加 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    clear();
                }
                else
                    this.Close();
            }
            if (ISURN == 0)
            {
                MessageBox.Show("客户创建失败,请检查是否录入有误！");
            }
        }
        private void read()
        {
            userlist_Server = new List<Addconnect_info>();

            Addconnect_info item = new Addconnect_info();
            item.mail = this.txname.Text;
            if (item.mail == null || item.mail == "")
            {
                errorProvider1.SetError(txname, "不能为空");
                return;
            }
            else
                errorProvider1.SetError(txname, String.Empty);

            item.name = this.txadress.Text;
            item.address = this.tshuihao.Text;
            item.phone = this.txbank.Text;
            item.cmname = txaccount.Text;
            item.weblink = txphone.Text;
            item.groupID = groupID;
            userlist_Server.Add(item);
        }


    }
}
