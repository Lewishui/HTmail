using clsBuiness;
using HT.DB;
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
    public partial class frmAddconnectGroup : Form
    {
        public frmAddconnectGroup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length < 1)
                return;


            List<AddconnectGroup_info> userlist_Server = new List<AddconnectGroup_info>();

            AddconnectGroup_info item = new AddconnectGroup_info();

            item.name = this.textBox1.Text;
            userlist_Server.Add(item);

            clsAllnew BusinessHelp = new clsAllnew();
           
            int ISURN = 0;
            ISURN = BusinessHelp.create_AddconnectGroup_Server(userlist_Server);
            if (ISURN == 1)
            {

                if (MessageBox.Show(" 客户创建成功 , 是否继续添加 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                }
                else
                    this.Close();
            }
            if (ISURN == 0)
            {
                MessageBox.Show("客户创建失败,请检查是否录入有误！");

            }
        }

    
    }
}
