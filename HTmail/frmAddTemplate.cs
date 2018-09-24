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
    public partial class frmAddTemplate : Form
    {
        string path;
        List<Template_info> userlist_Server;
        string groupID;
        Template_info m;
        public frmAddTemplate(Template_info tupe)
        {
            InitializeComponent();
            m = new Template_info();

            m = tupe;
            if (m != null)
            {
                this.txname.Text = m.subject;
                this.tshuihao.Text = m.body;
                txaccount.Text = m.acc;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog tbox = new OpenFileDialog();
            tbox.Multiselect = false;
            tbox.Filter = "Excel Files(*.xls,*.xlsx,*.xlsm,*.xlsb)|*.xls;*.xlsx;*.xlsm;*.xlsb";
            if (tbox.ShowDialog() == DialogResult.OK)
            {
                path = tbox.FileName;
                txaccount.Text = tbox.FileName;
            }
            if (path == null || path == "")
                return;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();

        }
        private void clear()
        {
            this.txname.Text = "";
            this.tshuihao.Text = "";
            txaccount.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            read();
            clsAllnew BusinessHelp = new clsAllnew();
            int ISURN=0;


            if (m == null)
                ISURN = BusinessHelp.create_mailTemplateServer(userlist_Server);
            else
            {
                string conditions = "";

                if (userlist_Server[0].subject != null)
                {
                    conditions += " subject ='" + userlist_Server[0].subject + "'";
                }
                if (userlist_Server[0] != null)
                {
                    conditions += " ,body ='" + userlist_Server[0].body + "'";
                }
                if (userlist_Server[0] != null)
                {
                    conditions += " ,acc ='" + userlist_Server[0].acc + "'";
                }

                if (userlist_Server[0] != null)
                {
                    conditions += " ,groupID ='" + userlist_Server[0].groupID + "'";
                }
                if (userlist_Server[0] != null)
                {
                    conditions += " ,PCid ='" + userlist_Server[0].PCid + "'";
                }
                conditions = "update mailTemplate set  " + conditions + " where _id = " + userlist_Server[0]._id + " ";
                ISURN = BusinessHelp.update_mailTemplateServer(conditions);
        
            }


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
            userlist_Server = new List<Template_info>();

            Template_info item = new Template_info();
            item.subject = this.txname.Text;
            if (item.subject == null || item.subject == "")
            {
                errorProvider1.SetError(txname, "不能为空");
                return;
            }
            else
                errorProvider1.SetError(txname, String.Empty);

            item.subject = this.txname.Text;
            item.body = this.tshuihao.Text;
            item.acc = this.txaccount.Text;
            item._id = m._id;
            item.groupID = m.groupID;
            item.PCid = m.PCid;
     

            item.groupID = groupID;
            userlist_Server.Add(item);
        }
    }
}
