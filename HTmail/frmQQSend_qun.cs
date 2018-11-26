using clsBuiness;
using HT.DB;
using Order.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTmail
{
    public partial class frmQQSend_qun : Form
    {
        // 后台执行控件
        private BackgroundWorker bgWorker;
        // 消息显示窗体
        private frmMessageShow frmMessageShow;
        // 后台操作是否正常完成
        private bool blnBackGroundWorkIsOK = false;
        //后加的后台属性显
        private bool backGroundRunResult;
        int rowcount;

        private bool isOneFinished = false;
        private DateTime StopTime;
        private System.Timers.Timer timerAlter;
        private Thread GetDataforRawDataThread;
        string adaewew;
        private bool IsRun = false;
        List<clsQQquninfo> OrderQUNlist_Server;
        int comboxi;
        string comboxiname;
        clsAllnew BusinessHelp;

        private void TimeControl(object sender, EventArgs e)
        {
            if (!IsRun)
            {
                IsRun = true;
                GetDataforRawDataThread = new Thread(TimeMethod);
                GetDataforRawDataThread.Start();
            }
        }


        private void NewMethod()
        {
            timerAlter = new System.Timers.Timer(40000);
            timerAlter.Elapsed += new System.Timers.ElapsedEventHandler(TimeControl);
            timerAlter.AutoReset = true;
            timerAlter.Start();
        }
        private void TimeMethod()
        {
            bool istrue = true;
            int dddindex = 1;
            if (listView1.Items.Count > 0)
            { 
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[2].Text == "是")
                    {
                        if (DateTime.Now.ToString("yyyy/MM/dd/HH/mm") == item.SubItems[3].Text)
                        {

                            istrue = mainSend(istrue, item);
                        }
                    }


                }
            }


        }
        public frmQQSend_qun()
        {
            InitializeComponent();

            //免费各类广告群   送大礼啦
            //Excel有偿做自动化工具   送大礼啦
            BusinessHelp = new clsAllnew();

            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Top = 0;
            this.Left = 0;
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            InitializeDataSource();
            NewMethod();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //   if (textBox1.Text == "") { MessageBox.Show("请先选择QQ/TM程序路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (textBox2.Text == "") { MessageBox.Show("请输入QQ/TM号码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (textBox3.Text == "") { MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (comboBox1.Text == "") { MessageBox.Show("请选择登陆状态", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string state;
            if (comboBox1.Text == "正常") state = "41"; else state = "40";

            //Base64EncoderClass.Base64Encoder base64 = new Base64EncoderClass.Base64Encoder();
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //string source = textBox3.Text;
            //byte[] pass;
            //pass = Encoding.Default.GetBytes(source);
            //md5.ComputeHash(pass);

            //string command = " /START QQUIN:" + textBox2.Text + " PWDHASH:" + base64.GetEncoded(md5.Hash) + " /STAT:" + state;

            DateTime dateclose = Convert.ToDateTime(this.dateTimePicker1.Value);

            string accrualselecttime = dateclose.ToString("yyyy/MM/dd/HH/mm").ToString();


            ListViewItem li = new ListViewItem();
            li.SubItems[0].Text = textBox2.Text;
            li.SubItems.Add(textBox3.Text);
            li.SubItems.Add(comboBox1.Text);
            li.SubItems.Add(accrualselecttime);
            listView1.Items.Add(li);
            List<clsQQquninfo> addOrderQUNlist_Server = new List<clsQQquninfo>();

            clsQQquninfo temp = new clsQQquninfo();

            temp.qun_name = textBox2.Text;
            temp.send_body = textBox3.Text;
            temp.is_timer = comboBox1.Text;
            temp.send_time = accrualselecttime;

            OrderQUNlist_Server.Add(temp);
            addOrderQUNlist_Server.Add(temp);

            int isok = BusinessHelp.create_QQqun_Server(addOrderQUNlist_Server);

            if (isok != 1)
            {
                MessageBox.Show("保存失败，请检查录入信息是否有误！");



            }
            InitializeDataSource();
        
        }
        private void InitialBackGroundWorker()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            bgWorker.ProgressChanged +=
                new ProgressChangedEventHandler(bgWorker_ProgressChanged);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                blnBackGroundWorkIsOK = false;
            }
            else if (e.Cancelled)
            {
                blnBackGroundWorkIsOK = true;
            }
            else
            {
                blnBackGroundWorkIsOK = true;
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (frmMessageShow != null && frmMessageShow.Visible == true)
            {
                //设置显示的消息
                frmMessageShow.setMessage(e.UserState.ToString());
                //设置显示的按钮文字
                if (e.ProgressPercentage == clsConstant.Thread_Progress_OK)
                {
                    frmMessageShow.setStatus(clsConstant.Dialog_Status_Enable);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                InitialBackGroundWorker();
                bgWorker.DoWork += new DoWorkEventHandler(ReadclaimFile);
                Control.CheckForIllegalCrossThreadCalls = false;
                bgWorker.RunWorkerAsync();
                // 启动消息显示画面
                frmMessageShow = new frmMessageShow(clsShowMessage.MSG_001,
                                                    clsShowMessage.MSG_007,
                                                    clsConstant.Dialog_Status_Disable);
                frmMessageShow.ShowDialog();
                // 数据读取成功后在画面显示

                if (blnBackGroundWorkIsOK)
                {



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:111" + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                throw ex;
            }


        }
        private void ReadclaimFile(object sender, DoWorkEventArgs e)
        {


            //导入程序集
            DateTime oldDate = DateTime.Now;

            sendinfo();

            DateTime FinishTime = DateTime.Now;  //   
            TimeSpan s = DateTime.Now - oldDate;
            string timei = s.Minutes.ToString() + ":" + s.Seconds.ToString();
            string Showtime = clsShowMessage.MSG_029 + timei.ToString();
            bgWorker.ReportProgress(clsConstant.Thread_Progress_OK, clsShowMessage.MSG_009 + "\r\n" + Showtime);
        }
        private void sendinfo()
        {
            bool istrue = true;
            int dddindex = 1;
            if (listView1.Items.Count > 0)
            {
                foreach (ListViewItem item in this.listView1.Items)
                {
                    bgWorker.ReportProgress(0, "发送中   :  " + dddindex.ToString() + "/" + listView1.Items.Count.ToString());

                    Thread.Sleep(1000);
                    if (item.SubItems[2].Text != "是")
                        istrue = mainSend(istrue, item);
                }
                dddindex++;
            }
        }

        private bool mainSend(bool istrue, ListViewItem item)
        {
            //for (int i = 0; i < item.SubItems.Count; i++)
            {
                moveFolder(item.SubItems[0].Text, item.SubItems[1].Text);

                //遍历所有查找到的进程
                isOneFinished = false;
                StopTime = DateTime.Now;
                istrue = false;
                while (!isOneFinished)
                {
                    Process[] pro = Process.GetProcesses();//获取已开启的所有进程

                    bool iscontains = false;
                    for (int ii = 0; ii < pro.Length; ii++)
                    {
                        if (pro[ii].ProcessName.ToString().Contains("Sendinfo"))
                        {
                            iscontains = true;

                            DateTime rq2 = DateTime.Now;  //结束时间
                            TimeSpan ts = rq2 - StopTime;
                            int timeTotal = ts.Minutes;
                            if (timeTotal >= 2)
                            {
                                //bgWorker1.ReportProgress(0, "系统错误，正在执行推出，请稍后自行检查数据源错误或运行环境问题！");
                                pro[ii].Kill();//结束进程
                                isOneFinished = true;
                                //Application.Exit();
                            }
                        }
                    }
                    if (iscontains == false)
                        isOneFinished = true;

                }
                Thread.Sleep(1000);
                //  if (i == 0)
                user_winauto("");
            }
            return istrue;
        }
        private static void user_winauto(string fajianren)
        {

            {
                string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "");
                System.Diagnostics.Process.Start("Sendinfo.exe", ZFCEPath);
            }

        }
        private void wirite_txt(string qunmingcheng, string body)
        {
            string A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\QQ\\sendto.txt";

            StreamWriter sw = new StreamWriter(A_Path);

            sw.WriteLine(qunmingcheng.Trim());

            sw.Flush();
            sw.Close();

            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\QQ\\body.txt";

            sw = new StreamWriter(A_Path);

            sw.WriteLine(body.Trim());


            sw.Flush();
            sw.Close();

        }
        private void moveFolder(string qunmingcheng, string body)
        {
            wirite_txt(qunmingcheng, body);
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\QQ";
            string dir = @"C:\Program Files (x86)\HTmail\System\\QQ";
            CopyFolder(path, dir);


        }
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }

        private void frmQQSend_qun_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon1.Visible = false;
        }

        private void frmQQSend_qun_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dateTimePicker1.Visible = true;

                dateTimePicker1.CustomFormat = "yyyy年MM月dd日HH时mm分";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.ShowUpDown = false;
            }
            else
            {
                dateTimePicker1.Visible = false;

            }
        }

        private void 删除本条ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var oids = GetOrderIdsBySelectedGridCell();
            BusinessHelp.deleteQQqun(OrderQUNlist_Server[comboxi].Order_id);

            OrderQUNlist_Server.RemoveAt(comboxi);

            // filename.RemoveAt(comboxi);
            listView1.Items.RemoveAt(comboxi);
            listView1.Items.Clear();


            InitializeDataSource();


        }
        private void InitializeDataSource()
        {
            OrderQUNlist_Server = new List<clsQQquninfo>();
            string strSelect = "select * from netlist";

            OrderQUNlist_Server = BusinessHelp.findQQqun(strSelect);
            this.listView1.Items.Clear();
          
            int Index = 1;
            foreach (clsQQquninfo item in OrderQUNlist_Server)
            {
                {
                    ListViewItem li = new ListViewItem();
                    li.SubItems[0].Text = item.qun_name;
                    li.SubItems.Add(item.send_body);
                    li.SubItems.Add(item.is_timer);
                    li.SubItems.Add(item.send_time);
                    listView1.Items.Add(li);

                    Index++;
                }
            }


        }
        private List<long> GetOrderIdsBySelectedGridCell()
        {

            //List<long> order_ids = new List<long>();
            ////var rows = GetSelectedRowsBySelectedCells(dataGridView1);
            //foreach (DataGridViewRow row in rows)
            //{
            //    var Diningorder = row.DataBoundItem as Addconnect_info;
            //    order_ids.Add((long)Convert.ToInt32(Diningorder._id));
            //}

            //return order_ids;
            return null;

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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int ddd = listView1.SelectedIndices[0];
                //   ShowImage(dailyResult[this.listView1.SelectedItems[0].Index].mark1);
                comboxi = ddd;
                comboxiname = OrderQUNlist_Server[this.listView1.SelectedItems[0].Index].qun_name.ToString();

            }
        }
    }
}
