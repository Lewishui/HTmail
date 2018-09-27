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
    public partial class frmFromReception : Form
    {
        List<AddconnectGroup_info> Orderinfolist_Server;
        AddconnectGroup_info m;
        private SortableBindingList<FromList_info> sortableOrderList;
        List<FromList_info> list_Server;
        public List<FromList_info> Addlist_Server;
        public string dd;

        int rowcount;

        public frmFromReception()
        {
            InitializeComponent();
            Resfres();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                var form = new frmFromTo(m._id);

                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {
            var form = new frmAddFromGroup();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Resfres();

            }
        }
        private void Resfres()
        {
            listBox1.Items.Clear();
            string strSelect = "select * from Fromgroup";

            clsAllnew BusinessHelp = new clsAllnew();
            Orderinfolist_Server = new List<AddconnectGroup_info>();
            Orderinfolist_Server = BusinessHelp.findFromGroup(strSelect);
            foreach (AddconnectGroup_info item in Orderinfolist_Server)
            {
                listBox1.Items.Add(item.name);
            }
            if (m != null && m._id != null && m._id != "")
                FINDdetail();

        }

        private void FINDdetail()
        {
            string strSelect = "select * from FromList where groupID='" + m._id + "'";


            clsAllnew BusinessHelp = new clsAllnew();
            list_Server = new List<FromList_info>();
            list_Server = BusinessHelp.findFromList(strSelect);

            sortableOrderList = new SortableBindingList<FromList_info>(list_Server);
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = sortableOrderList;
            this.toolStripLabel1.Text = "条数：" + sortableOrderList.Count.ToString();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
                return;

            m = new AddconnectGroup_info();

            m = Orderinfolist_Server[listBox1.SelectedIndex];


            FINDdetail();
        }

        private void toolStripDropDownButton4_Click(object sender, EventArgs e)
        {
            var form = new frmBath_uploadFrom(m._id);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Resfres();

            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" 确认删除这条信息 , 继续 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;

            var oids = GetOrderIdsBySelectedGridCell();
            for (int j = 0; j < oids.Count; j++)
            {
                var filtered = list_Server.FindAll(s => Convert.ToInt32(s._id) == oids[j]);
                clsAllnew BusinessHelp = new clsAllnew();
                //批量删 
                int istu = BusinessHelp.deleteFromList(filtered[0]._id.ToString());

                for (int i = 0; i < filtered.Count; i++)
                {
                    //单个删除

                    list_Server.Remove(list_Server.Where(o => o._id == filtered[i]._id).Single());
                    if (istu != 1)
                    {
                        MessageBox.Show("删除失败，请查看" + filtered[i].mail + filtered[i].mail, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BindDataGridView();

                }
            }
        }
        private void BindDataGridView()
        {
            sortableOrderList = new SortableBindingList<FromList_info>(list_Server);
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = sortableOrderList;
            this.toolStripLabel1.Text = "条数：" + sortableOrderList.Count.ToString();

        }

        private List<long> GetOrderIdsBySelectedGridCell()
        {

            List<long> order_ids = new List<long>();
            var rows = GetSelectedRowsBySelectedCells(dataGridView1);
            foreach (DataGridViewRow row in rows)
            {
                var Diningorder = row.DataBoundItem as FromList_info;
                if (Diningorder != null)
                    order_ids.Add((long)Convert.ToInt32(Diningorder._id));
            }

            return order_ids;
        }
        private IEnumerable<DataGridViewRow> GetSelectedRowsBySelectedCells(DataGridView dgv)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                rows.Add(cell.OwningRow);

            }
            rowcount = dgv.SelectedCells.Count;

            return rows.Distinct();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(" 确认删除这条信息 , 继续 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;

            clsAllnew BusinessHelp = new clsAllnew();
            //批量删 
            int istu = BusinessHelp.deleteFromgroup(m._id.ToString());

            Resfres();
            BindDataGridView();


        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {
            clsAllnew BusinessHelp = new clsAllnew();

            BusinessHelp.downcsv(dataGridView1);
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {
            Addlist_Server = new List<FromList_info>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue == true)
                {

                    FromList_info item = new FromList_info();

                    item.mail = dataGridView1.Rows[i].Cells["mail"].EditedFormattedValue.ToString();

                    Addlist_Server.Add(item);


                }
            }
            dd = "OK";

            this.Close();

        }

    }
}
