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
    public partial class frmSendpage : Form
    {
        // 后台执行控件
        private BackgroundWorker bgWorker;
        // 消息显示窗体
        private frmMessageShow frmMessageShow;
        // 后台操作是否正常完成
        private bool blnBackGroundWorkIsOK = false;
        //后加的后台属性显
        private bool backGroundRunResult;
        string path;
        List<string> filename = new List<string>();
        List<FromList_info> list_Server;
        List<FromList_info> Addlist_Server;
        List<Addconnect_info> Add_Sendlist_Server;
        private bool isOneFinished = false;
        private DateTime StopTime;
        private DateTime strFileName;

        public frmSendpage()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

            try
            {

                InitialBackGroundWorker();
                bgWorker.DoWork += new DoWorkEventHandler(BSendMail);

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
                return;
                throw ex;
            }




        }
        private void BSendMail(object sender, DoWorkEventArgs e)
        {
            DateTime oldDate = DateTime.Now;

            //初始化信息
            clsAllnew BusinessHelp = new clsAllnew();

            SendMail();


            DateTime FinishTime = DateTime.Now;
            TimeSpan s = DateTime.Now - oldDate;
            string timei = s.Minutes.ToString() + ":" + s.Seconds.ToString();
            string Showtime = clsShowMessage.MSG_029 + timei.ToString();
            bgWorker.ReportProgress(clsConstant.Thread_Progress_OK, clsShowMessage.MSG_009 + "\r\n" + Showtime);

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

        private void SendMail()
        {

            bool istrue = true;

            if (Addlist_Server != null && Addlist_Server.Count > 0)
            {
                int i = 0;
                foreach (FromList_info temp in Addlist_Server)
                {
                    i++;

               
                    if (temp.mail == null || temp.mail == "")
                        continue;

                    string strSelect = "select * from FromList where mail='" + temp.mail + "'";
                    clsAllnew BusinessHelp = new clsAllnew();
                    list_Server = new List<FromList_info>();
                    list_Server = BusinessHelp.findFromList(strSelect);

                    if (list_Server.Count == 0)
                    {
                        MessageBox.Show("没有找到此发送人,请在发件人设置中维护其信息后再次尝试", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string fajianren = "";
                    wirite_txt();
                    string path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail";
                    string dir = @"C:\Program Files (x86)\HTmail\System\\mail";
                    CopyFolder(path, dir);

                    fajianren = temp.mail;

                    #region 控制结束

                    Thread.Sleep(5000);

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
                            if (pro[ii].ProcessName.ToString().Contains("catch XL"))
                            {
                                iscontains = true;

                                DateTime rq2 = DateTime.Now;  //结束时间
                                TimeSpan ts = rq2 - StopTime;
                                int timeTotal = ts.Minutes;
                                if (timeTotal >= 20)
                                {
                                    //bgWorker1.ReportProgress(0, "系统错误，正在执行推出，请稍后自行检查数据源错误或运行环境问题！");
                                    //pro[i].Kill();//结束进程
                                    //isOneFinished = true;
                                    //Application.Exit();
                                }
                            }
                            else if (pro[ii].ProcessName.ToString().Contains("stop Q"))
                            {
                                iscontains = true;

                                DateTime rq2 = DateTime.Now;  //结束时间
                                TimeSpan ts = rq2 - StopTime;
                                int timeTotal = ts.Minutes;
                                if (timeTotal >= 20)
                                {
                                    //bgWorker1.ReportProgress(0, "系统错误，正在执行推出，请稍后自行检查数据源错误或运行环境问题！");
                                    //pro[i].Kill();//结束进程
                                    //isOneFinished = true;
                                    //Application.Exit();
                                }
                            }
                        }
                        if (iscontains == false)
                            isOneFinished = true;


                    }
                    #endregion

                    bgWorker.ReportProgress(0, "发送中 :  " + i.ToString() + "/" + Addlist_Server.Count.ToString());

                    if (fajianren != "" && fajianren.Contains("qq.com"))
                    {

                        string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "");
                        System.Diagnostics.Process.Start("stop Q.exe", ZFCEPath);
                    }
                    else if (fajianren != "" && fajianren.Contains("sina.com"))
                    {
                        string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""), "");
                        System.Diagnostics.Process.Start("catch XL.exe", ZFCEPath);
                    }
                }

            }
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

        private void wirite_txt()
        {
            string A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\sendto.txt";

            StreamWriter sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox1.Text);
            sw.Flush();
            sw.Close();


            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\ccto.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox5.Text);
            sw.Flush();
            sw.Close();

            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\fromto.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox2.Text);
            sw.Flush();
            sw.Close();


            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\subject.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox3.Text);
            sw.Flush();


            sw.Close();
            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\body.txt";

            sw = new StreamWriter(A_Path);
            sw.WriteLine(textBox4.Text);
            sw.Flush();
            sw.Close();

            A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\acc.txt";

            sw = new StreamWriter(A_Path);
            for (int i = 0; i < filename.Count; i++)
                sw.WriteLine(filename[i]);
            sw.Flush();
            sw.Close();

            if (list_Server.Count == 1 && list_Server[0].mail != null && list_Server[0].mail != "")
            {
                string mailaddress = "";
                if (list_Server[0].mail.Contains("qq.com"))
                    mailaddress = list_Server[0].mail.Substring(0, list_Server[0].mail.IndexOf("@"));
                else
                    mailaddress = list_Server[0].mail;
                A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\username.txt";
                sw = new StreamWriter(A_Path);
                sw.WriteLine(mailaddress);
                sw.Flush();
                sw.Close();



                A_Path = AppDomain.CurrentDomain.BaseDirectory + "System\\mail\\password.txt";
                sw = new StreamWriter(A_Path);
                sw.WriteLine(list_Server[0].password);
                sw.Flush();
                sw.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filename = new List<string>();

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
                    filename.Add(tbox.FileName);

                    listBox1.Items.Add(s);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";

            var form = new frmFromReception();
            Addlist_Server = new List<FromList_info>();
            form.ShowDialog();

            if (form.dd == "OK")
            {
                Addlist_Server = form.Addlist_Server;
                int d = 0;
                foreach (FromList_info ui in Addlist_Server)
                {
                    if (d != 0)
                        textBox2.Text += "," + ui.mail;
                    else
                        textBox2.Text += "" + ui.mail;
                    d++;

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            var form = new frmReception();
            Add_Sendlist_Server = new List<Addconnect_info>();
            form.ShowDialog();

            if (form.dd == "OK")
            {
                Add_Sendlist_Server = form.Addlist_Server;
                int d = 0;
                foreach (Addconnect_info ui in Add_Sendlist_Server)
                {
                    if (d != 0)
                        textBox1.Text += "," + ui.mail;
                    else
                        textBox1.Text += "" + ui.mail;
                    d++;

                }

            }
        }
    }
}
