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
    public partial class frmFromTo : Form
    {

        List<FromList_info> userlist_Server;
        string groupID;

        public frmFromTo(string groupID1)
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
            txname.Text = "";

            this.tshuihao.Text = "";
         
            txaccount.Text = "";
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            read();
            clsAllnew BusinessHelp = new clsAllnew();

            int ISURN = BusinessHelp.create_FromTo_Server(userlist_Server);
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
            userlist_Server = new List<FromList_info>();

            FromList_info item = new FromList_info();
            item.mail = this.txname.Text;
            if (item.mail == null || item.mail == "")
            {
                errorProvider1.SetError(txname, "不能为空");
                return;
            }
            else
                errorProvider1.SetError(txname, String.Empty);
     
            item.mail = this.txname.Text;
            item.password = this.tshuihao.Text;
            item.mark = txaccount.Text;          
            item.groupID = groupID;
            userlist_Server.Add(item);
        }


    }
}
