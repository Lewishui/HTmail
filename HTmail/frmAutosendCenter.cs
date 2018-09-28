using clsBuiness;
using DCTS.CustomComponents;
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
    public partial class frmAutosendCenter : Form
    {
        public List<Timer_info> Timer_Server;
        private SortableBindingList<Timer_info> sortableOrderList;

        public frmAutosendCenter()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            var form = new frmSendpage();
            form.ShowDialog();
            if (form.ddd == "OK")
            {
                Timer_Server = new List<Timer_info>();
                Timer_Server = form.Timer_Server;
                clsAllnew BusinessHelp = new clsAllnew();

                foreach (Timer_info uren in Timer_Server)
                    BusinessHelp.create_timer_Server(uren);



                NewMethod(Timer_Server);
            }
        }

        private void NewMethod(List<Timer_info> Timer_Server1)
        {
            listBox1.Items.Clear();
            int yifasong = 0;
            int weifasong = 0;

            List<Timer_info> filtered = Timer_Server.FindAll(s => s.status == "已发送");

            List<Timer_info> filtered1 = Timer_Server.FindAll(s => s.status == "未发送");

            this.listBox1.Items.Add("已发送  (" + filtered.Count() + ")");
            this.listBox1.Items.Add("未发送  (" + filtered1.Count() + ")");

            sortableOrderList = new SortableBindingList<Timer_info>(Timer_Server1);
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = sortableOrderList;
            this.toolStripLabel1.Text = "条数：" + sortableOrderList.Count.ToString();
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;
            string index = listBox1.SelectedItem.ToString();
            List<Timer_info> filtered = Timer_Server.FindAll(s => index.Contains(s.status));
            NewMethod(filtered);



        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {
            FindTimer();
            NewMethod(Timer_Server);
        }
        public void FindTimer()
        {
            clsAllnew BusinessHelp = new clsAllnew();

            string strSelect = "select * from Timer ";
            Timer_Server = new List<Timer_info>();
            Timer_Server = BusinessHelp.findTimer(strSelect);
        }
    
    }
}
